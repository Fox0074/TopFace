using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FizreFox
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _mainClip;

        void Start()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _audioSource.clip = _mainClip;
            _audioSource.Play();
            _startButton.GetComponent<Image>().DOFade(0,1f);
        }
    }
}