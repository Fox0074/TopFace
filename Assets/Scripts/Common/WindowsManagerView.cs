using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FizerFox
{
    internal class WindowStackItem
    {
        public Type Type { get; set; }
        public WindowOptions Options { get; set; }
        public BaseWindow Instance { get; set; }
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class WindowsManagerView : MonoBehaviour
    {
        [Inject]
        private BaseWindow.Factory _windowFactory;

        [SerializeField]
        private Image _backdrop;

        [SerializeField]
        private Button _backdropButton;

        private CanvasGroup _canvasGroup;
        private List<WindowStackItem> _stack;
        private List<WindowStackItem> _itemsToRemove;
        private WindowStackItem _activeItem;
        private Tweener _backdropTween;
        private bool _hidingAnimationInProcess;

        private void Awake()
        {
            _stack = new List<WindowStackItem>();
            _itemsToRemove = new List<WindowStackItem>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Initialize()
        {
            _backdropButton.onClick.AddListener(OnBackdropClick);
        }

        public void PushWindow(PushWindowSignal args)
        {
            WindowStackItem item = null;

            if (args.SecondValue.Single)
            {
                var stackItem = _stack.LastOrDefault(i => i.Type == args.FirstValue);
                if (stackItem != null)
                {
                    _stack.Remove(stackItem);
                    item = stackItem;
                }
            }

            if (item == null)
            {
                item = new WindowStackItem
                {
                    Type = args.FirstValue,
                    Options = args.SecondValue
                };
            }

            if (!args.SecondValue.Secondary)
                _stack.Add(item);
            else
                _stack.Insert(0, item);


            ActualizeWindowState();
        }

        public void PopWindow(PopWindowSignal args)
        {
            var stackItem = args.FirstValue == null ? _stack.Last() : _stack.LastOrDefault(item => item.Type == args.FirstValue);

            if (stackItem != null)
            {
                _stack.Remove(stackItem);
                _itemsToRemove.Add(stackItem);
                ActualizeWindowState();
            }
        }

        private void ActualizeWindowState()
        {
            if (_hidingAnimationInProcess)
                return;

            if (_backdropTween == null)
            {
                _backdropTween = _backdrop.DOFade(0f, .15f).From().Pause().SetAutoKill(false);
                _backdropTween.OnRewind(() => _backdrop.gameObject.SetActive(false));
            }

            if (_activeItem != null && _activeItem != _stack.LastOrDefault())
            {
                _hidingAnimationInProcess = true;
                _canvasGroup.interactable = false;
                _activeItem.Instance.Hide(() =>
                {
                    _activeItem.Instance.gameObject.SetActive(false);
                    _activeItem = null;
                    _hidingAnimationInProcess = false;
                    _canvasGroup.interactable = true;
                    ActualizeWindowState();
                });
            }
            else if (_activeItem == null && _stack.Count > 0)
            {
                var stackItem = _stack.Last();

                if (!stackItem.Options.WaitForAnimations)
                {
                    if (stackItem.Instance == null)
                    {
                        stackItem.Instance = _windowFactory.Create(stackItem.Type);
                        stackItem.Instance.transform.SetParent(transform, false);
                        stackItem.Instance.Initialize(stackItem.Options);
                        stackItem.Instance.CloseWindow += () => PopWindow(new PopWindowSignal(stackItem.Type));//TODO: рефактор
                    }
                    else
                    {
                        stackItem.Instance.gameObject.SetActive(true);
                    }

                    _activeItem = stackItem;
                    stackItem.Instance.Show(ActualizeWindowState);
                }
            }

            ActualizeBackdropState();
            RemoveUnusedWindows();
        }

        private void ActualizeBackdropState()
        {
            if (_activeItem != null || (_stack.Count > 0 && _stack.Last().Options.WaitForAnimations != true))
            {
                _backdrop.gameObject.SetActive(true);
                _backdropTween.PlayForward();
            }
            else
            {
                _backdropTween.PlayBackwards();
            }
        }

        private void RemoveUnusedWindows()
        {
            if (_activeItem == null)
            {
                foreach (var item in _itemsToRemove)
                {
                    if (item.Instance != null)
                        Destroy(item.Instance.gameObject);
                }

                _itemsToRemove.Clear();
            }
        }

        private void OnDestroy()
        {
            _backdropTween?.Kill();
        }

        private void OnBackdropClick()
        {
            if (_activeItem != null && !_activeItem.Options.Fixed)
                _activeItem.Instance.Close();
        }
    }
}