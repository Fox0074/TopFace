using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FizerFox.Editor
{
    public class EditorManager : MonoBehaviour
    {
        public static BlockTypes SelectedBlockType;

        [SerializeField]
        private AudioClip _editedAudioClip;

        [SerializeField]
        private SongTableViewController _gridController;

        [SerializeField]
        private List<BlockSelector> _selectors = new List<BlockSelector>();

        [SerializeField]
        private TimeLine _timeLine;


        private void Start()
        {
            _selectors.ForEach(x => x.Selected += OnSelectorSwitch);
            OnSelectorSwitch(SelectedBlockType);
            _gridController.Initialize(50);
            _timeLine.Initialize(_editedAudioClip.length);
        }

        private void OnSelectorSwitch(BlockTypes newType)
        {
            SelectedBlockType = newType;
            _selectors.ForEach(x => x.StateSwitched(SelectedBlockType));
        }
    }
}