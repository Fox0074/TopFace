using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace FizerFox.Editor
{
    public class PlayingHandle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _timer;

        [SerializeField]
        private HandleFollow _handleFollow;

        private Tween _currentTween;
        private bool _isPlaying;

        public void StartAnimation(float seconds)
        {
            var parent = transform.parent.GetComponent<RectTransform>();
            if (parent && !_isPlaying)
            {
                _isPlaying = true;
                _currentTween = GetComponent<RectTransform>().DOLocalMoveX(parent.rect.width, seconds).From(0).SetEase(Ease.Linear);
                _currentTween.OnUpdate(() => UpdateTimer(_currentTween.Elapsed()));
                _currentTween.OnComplete(() => _isPlaying = false);
               
            }
        }

        public void SetFollow(bool isFollow)
        {
            _handleFollow.SetFollow(isFollow);
        }

        public void StopAnimation()
        {
            _currentTween?.Kill();
            _isPlaying = false;
            _handleFollow.SetFollow(false);
        }

        private void UpdateTimer(float elapsed)
        {
            _timer.text = elapsed.ToString();
        }
    }
}