using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Zenject;

namespace FizerFox.Game
{
    public enum BlockType { Default, Long }
    [Serializable]
    public class SongModel<TK, TV>
    {
        [SerializeField] public TK Key;
        [SerializeField] public TV Value;
        [SerializeField] public BlockType Type;
    }

    public class GameController : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        [SerializeField]
        private CompleateLevelWindow _compleateLevelWindow;

        [SerializeField]
        private Transform _windowManager;

        [SerializeField]
        private TextMeshProUGUI _counter;

        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _mainClip;

        [SerializeField]
        private BaseBlockView _blockPrefab;

        [SerializeField]
        private LongBlockView _longBlockPrefab;

        [SerializeField]
        private List<Transform> _spawners = new List<Transform>();

        [SerializeField]
        public List<SongModel<float, int>> _songModel = new List<SongModel<float, int>>();

        private List<BaseBlockView> _fallingObjects = new List<BaseBlockView>();
        private CompleateLevelWindow _currentCompleateLevelWindow;
        private int _score;
        private bool _isPlaying;

        void Start()
        {
            _songModel.AddRange(_songModel);
            _songModel.AddRange(_songModel);
            _songModel.AddRange(_songModel);
            _songModel.AddRange(_songModel);

            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _isPlaying = true;
            _audioSource.clip = _mainClip;
            _audioSource.Play();
            _startButton.GetComponent<Image>().DOFade(0, 1f);

            StartCoroutine(WaitSongRoutine(_mainClip.length));
            StartCoroutine(PlaySongRoutine());
        }

        private IEnumerator PlaySongRoutine()
        {
            float lastPoint = 0;
            foreach (var point in _songModel)
            {
                yield return new WaitForSeconds(point.Key - lastPoint);
                lastPoint = point.Key;
                InsertBlock(point.Value, point.Type);
            }
        }

        private IEnumerator WaitSongRoutine(float clipTime)
        {
            yield return new WaitForSeconds(clipTime);

            if (_isPlaying)
                CompleateLevel(true);
        }

        private void InsertBlock(int track, BlockType type)
        {
            if (_fallingObjects.Count > 20)
            {
                var pulledBlock = _fallingObjects.FirstOrDefault(x => x.Type == type);
                _fallingObjects.Remove(pulledBlock);
                pulledBlock.StopFly();
                pulledBlock.transform.SetParent(_spawners[track]);
                pulledBlock.transform.localPosition = Vector3.zero;
                pulledBlock.StartFly();
                _fallingObjects.Add(pulledBlock);
            }
            else
            {
                var newBlock = Instantiate(type == BlockType.Default ? _blockPrefab : _longBlockPrefab, _spawners[track]);

                if (type == BlockType.Long)
                    newBlock.Type = BlockType.Long;

                newBlock.transform.localPosition = Vector3.zero;
                newBlock.StartFly();
                newBlock.OnClick += UpdateCounter;
                _fallingObjects.Add(newBlock);
            }
        }

        private void UpdateCounter()
        {
            _score++;
            _counter.text = _score.ToString();
        }

        private void CompleateLevel(bool isWin)
        {
            _isPlaying = false;
            if (isWin)
            {
                _counter.gameObject.SetActive(false);
                _signalBus.Fire(new PushWindowSignal(typeof(CompleateLevelWindow), new CompleateLevelWindowOptions()
                {
                    FlowersCount = _score / 53,
                    SongName = "Baby Shark",
                    Score = _score
                }));
            }
        }

        private void RestartGame()
        {
            _currentCompleateLevelWindow.OnRestart -= RestartGame;
            _currentCompleateLevelWindow.Close();
            _score = 0;
            _counter.gameObject.SetActive(true);
            _counter.text = _score.ToString();
            _fallingObjects.ForEach(x => Destroy(x.gameObject));
            _fallingObjects.Clear();
            StartGame();
        }
    }
}