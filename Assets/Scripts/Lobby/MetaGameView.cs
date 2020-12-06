using UnityEngine;
using Zenject;

namespace FizerFox.Meta
{
    public class MetaGameView : MonoBehaviour
    {
        private const string LobbyMusicSoundId = "lobby_music";
        private const string BoughtSoundId = "buy_any_item";

        //[Inject]
        //private IAudioPlayer _audioPlayer;

        public void Initialize()
        {
            //var musicAudioId = _audioPlayer.PlayMusic(LobbyMusicSoundId, true);
            //_audioPlayer.SetVolume(musicAudioId, 0f);
            //_audioPlayer.Fade(musicAudioId, 1f, 2f);
        }

        public void ItemBought()
        {
            //_audioPlayer.Play(BoughtSoundId);
        }
    }
}