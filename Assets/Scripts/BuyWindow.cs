using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;

namespace FizreFox
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BuyWindow : MonoBehaviour
    {
        [SerializeField]
        private Button _buyButton;
        [SerializeField]
        private Button _upButton;
        [SerializeField]
        private Button _downButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private TextMeshProUGUI _currentPlaceText;
        [SerializeField]
        private TextMeshProUGUI _newPlaceText;
        [SerializeField]
        private TMP_InputField _InputField;

        private int currentPlayerPlace;
        private int _pointerNewPlace;
        private CanvasGroup _canvasGroup;
        private List<User> _usersList = new List<User>();

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.DOFade(1,.25f).From(0).SetEase(Ease.InOutSine);
            transform.DOScale(1,.3f).From(.3f).SetEase(Ease.OutBack);

            _closeButton.onClick.AddListener(Close);
            _buyButton.onClick.AddListener(BuyButtonClick);
            _upButton.onClick.AddListener(UpButtonClick);
            _downButton.onClick.AddListener(DownButtonClick);

            var unsortedUsers = new List<User>();
            unsortedUsers.AddRange(TabsController.CurrentTab == TabsController.Tabs.world ? GameManager.UsersManager.WorldTopUsers : GameManager.UsersManager.FriendsUsers);
            unsortedUsers.Add(GameManager.UsersManager.CurrentUser);
            _usersList = unsortedUsers.OrderByDescending(x => x.Donate).ToList();
            currentPlayerPlace = _usersList.IndexOf(GameManager.UsersManager.CurrentUser);

            _InputField.text = 10.ToString();
        }

        private void OnRemove()
        {
            _closeButton.onClick.RemoveListener(Close);
            _buyButton.onClick.RemoveListener(BuyButtonClick);
            _upButton.onClick.RemoveListener(UpButtonClick);
            _downButton.onClick.RemoveListener(DownButtonClick);
        }

        public void Initialize()
        { 
            _currentPlaceText.text = "Текущее место в рейтинге: " + (currentPlayerPlace + 1);
            if (currentPlayerPlace > 0)
            {
                _pointerNewPlace = currentPlayerPlace;
                UpButtonClick();
            }
            else
            {
                _pointerNewPlace = currentPlayerPlace;
            }
        }

        private void UpButtonClick()
        {
            if (_pointerNewPlace > 0)
            {
                _pointerNewPlace--;
                _newPlaceText.text = "Новое место в рейтинге: " + (_pointerNewPlace + 1);
                _InputField.text = ((_usersList[_pointerNewPlace].Donate - GameManager.UsersManager.CurrentUser.Donate) + 1).ToString();
            }
        }

        private void DownButtonClick()
        {
            if (_pointerNewPlace < currentPlayerPlace)
            {
                _pointerNewPlace++;
                _newPlaceText.text = "Новое место в рейтинге: " + (_pointerNewPlace + 1);
                _InputField.text = ((_usersList[_pointerNewPlace].Donate - GameManager.UsersManager.CurrentUser.Donate) + 1).ToString();
            }
        }
        private void BuyButtonClick()
        {
            var donateValue = decimal.Parse(_InputField.text);

            var productData = new HappyGames.SocialAPI.SocialPurchaseData();
            productData.ProductTitle = donateValue + " Очков";
            productData.ProductDescription = "Купить " + donateValue + " очков";
            productData.ProductPrice = donateValue;
            GameManager.Current.SocialAPIManager.BuyProduct(productData.ProductPrice.ToString(), productData);
        }

        private void Close()
        {
            _canvasGroup.DOFade(0,.3f).OnComplete(() => Destroy(gameObject));
        }
    }
}