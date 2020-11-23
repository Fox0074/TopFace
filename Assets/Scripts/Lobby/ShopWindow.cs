using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Meta
{
    public class ShopWindow : MonoBehaviour
    {
        public static Action<string> TryBuyDecoration = delegate { };

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private List<DecorView> _decorViews = new List<DecorView>();

        public void Initialize()
        {
            _closeButton.onClick.AddListener(Close);

            GetComponent<CanvasGroup>().DOFade(1, .5f).From(0).SetEase(Ease.InOutSine);

            foreach (var decorView in _decorViews)
            {
                var isBuy = LobbyController.PlayerDecorations.Contains(decorView.GetDecorType());
                decorView.Initialize(350, !isBuy);
                decorView.BuyButtonClick += Close;
            }
        }

        private void Close()
        {
            TryBuyDecoration -= (x) => Close();
            GetComponent<CanvasGroup>().DOFade(0, .5f).SetEase(Ease.InOutSine).OnComplete(() => Destroy(gameObject));
        }
    }
}