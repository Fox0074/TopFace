using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizreFox
{
    public class BuyProductButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnRemove()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        private void OnClick()
        {
            var productData = new HappyGames.SocialAPI.SocialPurchaseData();
            productData.ProductTitle = "10 Очков";
            productData.ProductDescription = "Купить 10 очков";
            productData.ProductPrice = 10;
            GameManager.Current.SocialAPIManager.BuyProduct("1", productData);
        }
    }
}