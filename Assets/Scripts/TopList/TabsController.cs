using System;
using UnityEngine;
using UnityEngine.UI;

namespace FizerFox
{
    public class TabsController : MonoBehaviour
    {
        public enum Tabs {world,friends}
        public static event Action OnWorldTabButtonClick = delegate { };
        public static event Action OnFriendsTabButtonClick = delegate { };

        public static Tabs CurrentTab = Tabs.world;

        [SerializeField]
        private Button _worldButton;

        [SerializeField]
        private Button _friendsButton;

    
        private void Awake()
        {
            _worldButton.onClick.AddListener(SetWorldTab);
            _friendsButton.onClick.AddListener(SetFriendsTab);
        }

        private void OnRemove()
        {
            _worldButton.onClick.RemoveListener(SetWorldTab);
            _friendsButton.onClick.RemoveListener(SetFriendsTab);
        }
        private void SetWorldTab()
        {
            if (CurrentTab != Tabs.world)
            {
                OnWorldTabButtonClick();
                CurrentTab = Tabs.world;
            }
        }

        private void SetFriendsTab()
        {
            if (CurrentTab != Tabs.friends)
            {
                OnFriendsTabButtonClick();
                CurrentTab = Tabs.friends;
            }
        }
    }
}