using UnityEngine;
using TMPro;

namespace FizerFox
{
    public class PlayerCell : MonoBehaviour
    {
        [SerializeField]
        private NetworkImage _avatarImage;

        [SerializeField]
        private TextMeshProUGUI _playerName;

        [SerializeField]
        private TextMeshProUGUI _donateView;

        [SerializeField]
        private TextMeshProUGUI _positionText;

        private User _user;
        public void Initialize(User user, int position)
        {
            _user = user;
            user.DataUpdated +=  UpdateUserData;
            _positionText.text = position.ToString();
            UpdateUserData();
        }

        private void UpdateUserData()
        {
             _avatarImage.LoadImage(_user.Avatar);
            _playerName.text = _user.UserName;
            _donateView.text = _user.Donate.ToString();
        }

        private void OnRemove()
        {
            _user.DataUpdated -=  UpdateUserData;
        }
    }
}