using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FizerFox.Editor
{
    public class SongTableViewController : MonoBehaviour
    {
        [SerializeField]
        private GridBlockLine _gridBlockLinePrefab;

        [SerializeField]
        private Transform _gridBlockLineParent;

        private List<GridBlockLine> _gridBlockLines = new List<GridBlockLine>();

        public void Initialize(int linesCount)
        {
            for (int i = 0; i < linesCount; i++)
            {
                var newLine = Instantiate(_gridBlockLinePrefab, _gridBlockLineParent);
                _gridBlockLines.Add(newLine);
            }
        }
    }
}