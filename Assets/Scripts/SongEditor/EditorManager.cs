using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox.Editor
{
    public class EditorManager : MonoBehaviour
    {
        public static BlockTypes SelectedBlockType;

        [SerializeField]
        private int _BPM;

        [SerializeField]
        private AudioClip _editedAudioClip;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private SongTableViewController _gridController;

        [SerializeField]
        private List<BlockSelector> _selectors = new List<BlockSelector>();

        [SerializeField]
        private TimeLine _timeLine;

        [SerializeField]
        private Button _playingButton;

        [SerializeField]
        private Button _followButton;

        private bool _isPlaying;
        private bool _isFollow;

        private void Start()
        {
            _playingButton.onClick.AddListener(PlaySongButtonClick);
            _followButton.onClick.AddListener(SwitchFollowState);
            _audioSource.clip = _editedAudioClip;

            var linesCount = Mathf.CeilToInt((_BPM / 60) * _editedAudioClip.length);
            _selectors.ForEach(x => x.Selected += OnSelectorSwitch);

            OnSelectorSwitch(SelectedBlockType);
            _gridController.Initialize(linesCount, _editedAudioClip.length);

            StartCoroutine(TimeLineRoutine());
        }

        public void PlaySongButtonClick()
        {
            if (!_isPlaying)
            {
                _gridController.PlayHandle(_editedAudioClip.length);
                _audioSource.Play();
                _isPlaying = true;
            }
            else
            {
                _gridController.StopHandle();
                _audioSource.Stop();
                _isPlaying = false;
            }
        }

        private void SwitchFollowState()
        {
            _isFollow = !_isFollow;
            _gridController.SetHandleFollow(_isFollow);
        }

        private IEnumerator TimeLineRoutine()
        {
            yield return null;

            Dictionary<Vector2, float> test = new Dictionary<Vector2, float>();
            var lines = _gridController.GetGridBlockLines();
            foreach (var line in lines)
            {
                test.Add(line.GetPosition(), line.StartTime);
            }

            _timeLine.Initialize(test);
        }

        private void OnSelectorSwitch(BlockTypes newType)
        {
            SelectedBlockType = newType;
            _selectors.ForEach(x => x.StateSwitched(SelectedBlockType));
        }
    }
}