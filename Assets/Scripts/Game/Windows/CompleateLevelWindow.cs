using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

namespace FizerFox.Game
{
    public class CompleateLevelWindowOptions : WindowOptions
    {
        public int FlowersCount;
        public string SongName;
        public int Score;
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class CompleateLevelWindow : BaseWindow
    {
        public Action OnRestart = delegate { };

        [SerializeField]
        private TextMeshProUGUI _songName;

        [SerializeField]
        private TextMeshProUGUI _scoreLabel;

        [SerializeField]
        private Button _homeButton;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private List<GameObject> _flowers;

        private CanvasGroup _canvasGroup;

        public override void Initialize(WindowOptions options)
        {
            var windowOptions = options as CompleateLevelWindowOptions;

            for (int i = 0; i < windowOptions.FlowersCount; i++)
                _flowers[i].gameObject.SetActive(true);

            _songName.text = windowOptions.SongName;
            _scoreLabel.text = windowOptions.Score.ToString();
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(1,1f).SetEase(Ease.InOutSine).OnComplete(() => _canvasGroup.interactable = true);
            _homeButton.onClick.AddListener(OnHomeButtonClick);
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnHomeButtonClick()
        {
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        }

        private void OnRestartButtonClick()
        {
            OnRestart.Invoke();
        }
    }
}