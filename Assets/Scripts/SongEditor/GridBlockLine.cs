using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Editor
{
    public class GridBlockLine : MonoBehaviour
    {
        public float StartTime { get; set; }
        public float Speed { get; set; } = 1f;

        [SerializeField]
        private RectTransform _lineRect;

        [SerializeField]
        private Button _topSelector;

        [SerializeField]
        private List<GridBlock> _blocks = new List<GridBlock>();

        public void Initialize(float startTime)
        {
            StartTime = startTime;
        }

        public Vector2 GetPosition()
        {
            return _lineRect.localPosition;
        }

        private void Awake()
        {
            _blocks.ForEach(x => x.OnClick += OnButtonClick);
        }

        private void OnButtonClick(GridBlock block)
        {
            block.SetType(EditorManager.SelectedBlockType);
        }
    }
}