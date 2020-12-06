using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Editor
{
    public class SongTableViewController : MonoBehaviour
    {
        [SerializeField]
        private GridBlockLine _gridBlockLinePrefab;

        [SerializeField]
        private Transform _gridBlockLineParent;

        [SerializeField]
        private PlayingHandle _playingHandle;

        private List<GridBlockLine> _gridBlockLines = new List<GridBlockLine>();

        public void Initialize(int linesCount, float seconds)
        {
            var lengthStep = seconds / linesCount;
            for (int i = 0; i < linesCount; i++)
            {
                var newLine = Instantiate(_gridBlockLinePrefab, _gridBlockLineParent);
                newLine.Initialize(i * lengthStep);
                _gridBlockLines.Add(newLine);
            }
        }

        public void SetHandleFollow(bool isFollow)
        {
            _playingHandle.SetFollow(isFollow);
        }

        public List<GridBlockLine> GetGridBlockLines()
        {
            return _gridBlockLines;
        }

        public void PlayHandle(float seconds)
        {
            _playingHandle.StartAnimation(seconds);
        }

        public void StopHandle()
        {
            _playingHandle.StopAnimation();
        }
    }
}