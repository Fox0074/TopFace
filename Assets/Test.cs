using UnityEngine;
using UnityEngine.UI;
using Zenject;
using FizerFox.Game;

public class Test : MonoBehaviour
{
    [Inject]
    private SwitchToGameCommand SignalBus;

    [Inject]
    private Game game;

    [SerializeField]
    private AudioClip audioClip;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Button button;

    void Start()
    {
        button.onClick.AddListener(test);
        if (game.Level != null)
            audioSource.clip = game.Level.AudioClip;

    }

    private void test()
    {
        SignalBus.Execute(new FizerFox.LevelData 
        { 
            AudioClip = audioClip,
            Author = "Autor",
            Difficulty = FizerFox.SongDifficulty.Easy,
            Title = "Test"

        });
    }
}
