using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Editor
{
    public class HandleFollow : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private Transform _handle;

        private bool _isFollow;
        private float _centerX;

        private void Start()
        {
            _centerX = GetComponent<RectTransform>().rect.width / 2;
        }

        public void SetFollow(bool isFollow)
        {
            _isFollow = isFollow;
        }

        private void Update()
        {
            if (_isFollow)
            {
                float normalizedPosition = (_handle.localPosition.x - _centerX - 200) / _scrollRect.content.sizeDelta.x;
                normalizedPosition = Mathf.Min(1, Mathf.Max(0, normalizedPosition));
                _scrollRect.horizontalNormalizedPosition = normalizedPosition;
            }
        }
    }
}