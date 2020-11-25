using DG.Tweening;
using FizerFox.Game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FizerFox
{
    public class LongBlockView : BaseBlockView, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private Color _color;
        private bool _isDown;

        public LongBlockView() : base()
        {
            Type = BlockType.Long;
        }

        protected override void Start()
        {
            _interacleble = true;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (_interacleble)
            {
                OnClick.Invoke();
                _color = _image.color;
                _image.color = Color.yellow;
                _interacleble = false;
                _isDown = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isDown)
            {
                OnPointerDown();
                _isDown = false;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isDown)
            {
                OnPointerDown();
                _isDown = false;
            }
        }

        private void OnPointerDown()
        {
            _image.DOFade(.5f, .3f);
            _image.color = _color;
        }
    }
}