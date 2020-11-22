using DG.Tweening;
using FizreFox.Game;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FizreFox
{
    public class BaseBlockView : MonoBehaviour, IPointerDownHandler
    {
        public Action OnClick = delegate { };
        public BlockType Type;

        [SerializeField]
        protected Image _image;

        protected bool _interacleble;
        protected bool _isFlying;
        protected float _speed = 500f;
        protected float _lifeTime = 10f;
        protected float _flyingTime;

        public BaseBlockView()
        {
            Type = BlockType.Default;
        }
        public virtual void StartFly()
        {
            _isFlying = true;
            _interacleble = true;
            _image.DOFade(1, 0);
            _flyingTime = 0;
        }

        public virtual void StopFly()
        {
            _isFlying = false;
        }

        public virtual void SetSpeed(float value)
        {
            _speed = value;
        }

        protected virtual void Start()
        {
            _interacleble = true;
        }


        protected virtual void Update()
        {
            if (_isFlying)
            {
                transform.localPosition += new Vector3(0, -1, 0) * _speed * Time.deltaTime;
                _flyingTime += Time.deltaTime;

                if (_flyingTime >= _lifeTime) 
                    StopFly();
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (_interacleble)
            {
                OnClick.Invoke();
                _image.DOFade(.5f, .3f).SetEase(Ease.InOutSine);
                _interacleble = false;
            }
        }
    }
}