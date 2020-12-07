using UnityEngine;
using UnityEngine.UI;
using System;

namespace FizerFox.Editor
{
    public class GridBlock : MonoBehaviour
    {
        public Action<GridBlock> OnClick = delegate { };

        public BlockTypes BlockType { get; private set; }

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnClick.Invoke(this));
        }

        public void SetType(BlockTypes newType)
        {
            BlockType = newType;

            switch (newType)
            {
                case BlockTypes.Empty:
                    _image.color = Color.white;
                    break;
                case BlockTypes.Simple:
                    _image.color = Color.red;
                    break;
                case BlockTypes.Long:
                    _image.color = Color.yellow;
                    break;
            }
        }

    }
}