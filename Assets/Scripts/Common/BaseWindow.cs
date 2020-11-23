using System;
using UnityEngine;
using Zenject;

namespace FizerFox
{
    public class WindowOptions
    {
        public static readonly WindowOptions Empty = new WindowOptions();

        public bool Fixed { get; set; }
        public bool Single { get; set; }
        public bool Secondary { get; set; }
        public bool Stacked { get; set; }
        public bool WaitForAnimations { get; set; }
    }

    public abstract class BaseWindow : MonoBehaviour
    {
        public event Action CloseWindow = delegate { };
        public WindowOptions Options { get; private set; }
        public bool Closed { get; private set; }

        public virtual void Initialize(WindowOptions options)
        {
            Options = options;
            Closed = false;
        }

        public virtual void Show(Action onComplete)
        {
            onComplete();
        }

        public virtual void Hide(Action onComplete)
        {
            onComplete();
        }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void OnClose() { }

        protected void PlayShowSound()
        {
            //TODO: звуки ч/з AudioPlayer
        }

        protected void PlayHideSound()
        {
            //TODO: звуки ч/з AudioPlayer
        }

        public void Close()
        {
            if (Closed) return;
            Closed = true;
            OnClose();
            CloseWindow();
        }

        public class Factory : PlaceholderFactory<Type, BaseWindow> { }
    }
}