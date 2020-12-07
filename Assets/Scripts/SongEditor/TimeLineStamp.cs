using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FizerFox.Editor
{
    public class TimeLineStamp : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private TextMeshProUGUI _timeText;

        private float _time;

        public void SetText(float seconds)
        {
            _time = seconds;
            TimeSpan ts = TimeSpan.FromSeconds(seconds);
            _timeText.text = String.Format("{0:00}:{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

        }
    }
}
