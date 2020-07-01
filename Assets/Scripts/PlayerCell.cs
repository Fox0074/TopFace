using UnityEngine;
using TMPro;

namespace FizreFox
{
    public class PlayerCell : MonoBehaviour
    {
        [SerializeField]
        private NetworkImage _avatarImage;

        [SerializeField]
        private TextMeshProUGUI _playerName;

        [SerializeField]
        private TextMeshProUGUI _donateView;

        private User _user;
        public void Initialize(User user)
        {
            _user = user;
            user.DataUpdated +=  UpdateUserData;
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