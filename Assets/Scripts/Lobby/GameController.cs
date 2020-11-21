using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace FizreFox
{
    [Serializable]
    public class SongModel<TK, TV>
    {
        [SerializeField] public TK Key;
        [SerializeField] public TV Value;
    }

    public class GameController : MonoBehaviour
    {
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
        private List<Transform> _spawners = new List<Transform>();

        [SerializeField]
        public List<SongModel<float, int>> _songModel = new List<SongModel<float, int>>();

        private List<BaseBlockView> _fallingObjects = new List<BaseBlockView>();
        private int _score;

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
            _audioSource.clip = _mainClip;
            _audioSource.Play();
            _startButton.GetComponent<Image>().DOFade(0,1f);

            StartCoroutine(PlaySongRoutine());
        }

        private IEnumerator PlaySongRoutine()
        {
            float lastPoint = 0;
            foreach (var point in _songModel)
            {
                yield return new WaitForSeconds(point.Key - lastPoint);
                lastPoint = point.Key;
                InsertBlock(point.Value);
            }
        }

        private void InsertBlock(int track)
        {
            if (_fallingObjects.Count > 20)
            {
                var pulledBlock = _fallingObjects[0];
                _fallingObjects.Remove(pulledBlock);
                pulledBlock.StopFly();
                pulledBlock.transform.SetParent(_spawners[track]);
                pulledBlock.transform.localPosition = Vector3.zero;
                pulledBlock.StartFly();
                _fallingObjects.Add(pulledBlock);
            }
            else
            {
                var newBlock = Instantiate(_blockPrefab, _spawners[track]);
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
    }
}