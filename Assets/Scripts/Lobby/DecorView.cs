using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace FizerFox.Meta
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DecorView : MonoBehaviour
    {
        public Action BuyButtonClick = delegate { };

        [SerializeField]
        private string _decorType;

        [SerializeField]
        private GameObject _lockView;

        [SerializeField]
        private TextMeshProUGUI _price;

        [SerializeField]
        private Button _buyButton;

        public void Initialize(int price, bool isLock)
        {
            _buyButton.onClick.AddListener(() => BuyButtonClick.Invoke());
            BuyButtonClick += () => ShopWindow.TryBuyDecoration.Invoke(_decorType);

            _price.text = price + " <sprite name=jewel>";
            _lockView.SetActive(isLock);
            _price.gameObject.SetActive(isLock);

            if (isLock)
            {
                GetComponent<CanvasGroup>().DOFade(.5f, .5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            }
        }

        public string GetDecorType()
        {
            return _decorType;
        }
    }
}