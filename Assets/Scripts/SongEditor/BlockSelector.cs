using System;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Editor
{
    public class BlockSelector : MonoBehaviour
    {
        public Action<BlockTypes> Selected = delegate { };
        public bool IsSelected { get; private set; }

        [SerializeField]
        private Button _secetButton;

        [SerializeField]
        private GameObject _secetedImage;

        [SerializeField]
        private BlockTypes _blockType;

        private void Awake()
        {
            _secetButton.onClick.AddListener(OnButtonClick);
        }

        public void StateSwitched(BlockTypes type)
        {
            var isActive = type == _blockType;

            IsSelected = isActive;
            _secetedImage.SetActive(isActive);
        }

        private void OnButtonClick()
        {
            if (!IsSelected)
            {
                Selected(_blockType);
                IsSelected = true;
            }
        }
    }
}