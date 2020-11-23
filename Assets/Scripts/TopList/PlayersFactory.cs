using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FizerFox
{
    public class PlayersFactory : MonoBehaviour
    {
        [SerializeField]
        private PlayerCell _playerCellPrefab;

        [SerializeField]
        private Transform _parent;

        private List<PlayerCell> _cellViews = new List<PlayerCell>();

        private void Start()
        {
            TabsController.OnFriendsTabButtonClick += SetFriendsUsers;
            TabsController.OnWorldTabButtonClick += SetWorldUsers;
        }

        public void AddPlayer(User user, int position)
        {
            var playerView = Instantiate(_playerCellPrefab, _parent);
            playerView.Initialize(user, position);
            playerView.transform.localScale = Vector3.one;
            _cellViews.Add(playerView);
        }

        public void SetFriendsUsers()
        {
            ClearViews();

            var usersList = new List<User>();
            usersList.AddRange(GameManager.UsersManager.FriendsUsers);
            usersList.Add(GameManager.UsersManager.CurrentUser);
            var sortedUsers = usersList.OrderByDescending(x => x.Donate);
            var pos = 0;

            foreach (var user in sortedUsers)
            {
                pos++;
                AddPlayer(user,pos);
            }
        }

        public void SetWorldUsers()
        {
            ClearViews();

            var usersList = new List<User>();
            usersList.AddRange(GameManager.UsersManager.WorldTopUsers);
            usersList.Add(GameManager.UsersManager.CurrentUser);
            var sortedUsers = usersList.OrderByDescending(x => x.Donate);
            var pos = 0;

            foreach (var user in sortedUsers)
            {
                pos++;
                AddPlayer(user, pos);
            }
        }

        private void ClearViews()
        {
            foreach(var item in _cellViews)
            {
                Destroy(item.gameObject);
            }
            _cellViews.Clear();
        }
    }
}