using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using DG.Tweening;

namespace FizerFox.Game
{
    public class BackGroundView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Image _switchedImage;

        [SerializeField]
        private SpriteAtlas _backgroundAtlas;

        private void Awake()
        {
            _switchedImage.gameObject.SetActive(false);
        }

        public void SwitchBackGround(SwitshBackGroundSignal signal)
        {
            var sprite = _backgroundAtlas.GetSprite(signal.SpriteName);
            _switchedImage.sprite = sprite;
            _switchedImage.gameObject.SetActive(true);

            _switchedImage.DOFade(1,.5f).From(0).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                _switchedImage.gameObject.SetActive(false);
                _image.sprite = sprite;
            });
        }
    }
}