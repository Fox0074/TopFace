using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FizerFox.Editor
{
    public class TimeLine : MonoBehaviour
    {
        public Action StartTimeLineUpdate = delegate { };

        [SerializeField]
        private int _countStamps;

        [SerializeField]
        private RectTransform _timeLine;

        [SerializeField]
        private TimeLineStamp _timeLineStamp;

        private List<TimeLineStamp> _stamps = new List<TimeLineStamp>();
        private float _length;

        public void Initialize(float length)
        {
            _length = length;
            StartTimeLineUpdate += UpdateState;

            StartCoroutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            yield return null;

            for (int i = 0; i < _countStamps; i++)
            {
                var newStamp = Instantiate(_timeLineStamp, _timeLine);
                newStamp.SetText(i * (_length / _countStamps));
                newStamp.transform.localPosition = new Vector3(i * (_timeLine.rect.size.x / _countStamps), 0, 0);

                _stamps.Add(newStamp);
            }
        }

        private void UpdateState()
        {
            foreach(var stamp in _stamps)
            {
                var index = _stamps.IndexOf(stamp);
                stamp.SetText(index * (_length / _countStamps));
                stamp.transform.localPosition = new Vector3(index * (_timeLine.rect.size.x / _countStamps), 0, 0);
            }
        }
    }
}