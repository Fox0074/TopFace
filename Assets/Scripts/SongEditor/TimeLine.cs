using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private Dictionary<Vector2, float> _stampsData = new Dictionary<Vector2, float>();

        public void Initialize(Dictionary<Vector2,float> stampsData)
        {
            _stampsData = stampsData;
            StartTimeLineUpdate += UpdateState;

            StartCoroutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            yield return null;

            foreach (var stampData in _stampsData)
            {
                var newStamp = Instantiate(_timeLineStamp, _timeLine);
                newStamp.SetText(stampData.Value);
                newStamp.transform.localPosition = stampData.Key;

                _stamps.Add(newStamp);
            }
        }

        private void UpdateState()
        {
            if (_stampsData.Count == _stamps.Count)
            {
                foreach (var stamp in _stamps)
                {
                    var index = _stamps.IndexOf(stamp);
                    var stampData = _stampsData.ElementAt(index);
                    stamp.SetText(stampData.Value);
                    stamp.transform.localPosition = stampData.Key;
                }
            }
            else
            {
                _stamps.ForEach(x => Destroy(x));
                StartCoroutine(InitializeRoutine());
            }
        }
    }
}