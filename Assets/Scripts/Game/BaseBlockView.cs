using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizreFox
{
    public class BaseBlockView : MonoBehaviour
    {
        public Action OnClick = delegate { };

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Button _button;

        private bool _interacleble;
        private bool _isFlying;
        private float _speed = 500f;
        private float _lifeTime = 10f;
        private float _flyingTime;


        public void StartFly()
        {
            _isFlying = true;
            _interacleble = true;
            _image.DOFade(1, 0);
            _flyingTime = 0;
        }

        public void StopFly()
        {
            _isFlying = false;
        }

        public void SetSpeed(float value)
        {
            _speed = value;
        }

        private void Start()
        {
            _interacleble = true;
            _button.onClick.AddListener(OnButtonClick);
        }


        private void Update()
        {
            if (_isFlying)
            {
                transform.localPosition += new Vector3(0, -1, 0) * _speed * Time.deltaTime;
                _flyingTime += Time.deltaTime;

                if (_flyingTime >= _lifeTime) 
                    StopFly();
            }
        }

        private void OnButtonClick()
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