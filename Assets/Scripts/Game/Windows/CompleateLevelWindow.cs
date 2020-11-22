using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

namespace FizreFox.Game
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CompleateLevelWindow : MonoBehaviour
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

        public void Initialize(int flowersCount, string songName, int score)
        {
            for (int i = 0; i < flowersCount; i++)
                _flowers[i].gameObject.SetActive(true);

            _songName.text = songName;
            _scoreLabel.text = score.ToString();
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

        public void Close()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0, 1f).SetEase(Ease.InOutSine);
            Destroy(gameObject);
        }
    }
}