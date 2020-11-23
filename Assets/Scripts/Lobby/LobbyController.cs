using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

namespace FizerFox.Meta
{
    public class LobbyController : MonoBehaviour
    {
        public static List<string> PlayerDecorations = new List<string>();
        public static int PlayerCurrency = 500;

        [SerializeField]
        private TextMeshProUGUI _currencyLabel;

        [SerializeField]
        private List<DecorView> _decorViews = new List<DecorView>();

        [SerializeField]
        private Transform _decorationsContainer;

        [SerializeField]
        private Button _shopButton;

        [SerializeField]
        private ShopWindow _shopPrefab;

        [SerializeField]
        private Transform _windowManager;

        private void Start()
        {
            ShopWindow.TryBuyDecoration += TryBuyDecoration;
            _currencyLabel.text = PlayerCurrency.ToString();
            PlayerDecorations.ForEach(x => InstansDecoration(x));
            _shopButton.onClick.AddListener(ShowShopWindow);
        }

        private void ShowShopWindow()
        {
            var window = Instantiate(_shopPrefab, _windowManager);
            window.Initialize();
        }

        //TODO: Заменить в первую очередь
        private void TryBuyDecoration(string type)
        {
            var buyDecor = _decorViews.FirstOrDefault(x => x.GetDecorType() == type);
            if (buyDecor == null) return;

            if (PlayerCurrency >= 350)
            {
                PlayerCurrency -= 350;
                PlayerDecorations.Add(type);
                InstansDecoration(type);
                _currencyLabel.text = PlayerCurrency.ToString();
            }
        }

        private void InstansDecoration(string type)
        {
            var decor = _decorViews.FirstOrDefault(x => x.GetDecorType() == type);
            var instanseDecor = Instantiate(decor, _decorationsContainer);
            instanseDecor.Initialize(350, false);
        }

    }
}