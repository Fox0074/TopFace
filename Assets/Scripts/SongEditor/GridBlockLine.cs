using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Editor
{
    public class GridBlockLine : MonoBehaviour
    {
        [SerializeField]
        private Button _topSelector;

        [SerializeField]
        private List<Button> _buttons = new List<Button>();

        private void Awake()
        {
            _buttons.ForEach(x => x.onClick.AddListener(() => OnButtonClick(x.gameObject)));
        }

        private void OnButtonClick(GameObject block)
        {
            var image = block.GetComponent<Image>();

            if (image)
            {
                switch (EditorManager.SelectedBlockType)
                {
                    case BlockTypes.Empty:
                        image.color = Color.white;
                        break;
                    case BlockTypes.Simple:
                        image.color = Color.red;
                        break;
                    case BlockTypes.Long:
                        image.color = Color.yellow;
                        break;
                }
            }
        }
    }
}