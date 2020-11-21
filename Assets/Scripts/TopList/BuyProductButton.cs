using UnityEngine;
using UnityEngine.UI;

namespace FizreFox
{
    public class BuyProductButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private BuyWindow _buyWindowPrefab;

        [SerializeField]
        private Transform _parentWindow;
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
            var buyWindow = Instantiate(_buyWindowPrefab,_parentWindow);
            buyWindow.Initialize();
        }
    }
}