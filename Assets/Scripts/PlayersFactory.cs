using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFactory : MonoBehaviour
{
    [SerializeField]
    private PlayerCell _playerCellPrefab;

    [SerializeField]
    private Transform _parent;


    public void Initialized(Dictionary<User, Sprite> playersData)
    {
        foreach (var data in playersData)
        {
            var playerView = Instantiate(_playerCellPrefab, _parent);
            playerView.Initialize(data.Value, data.Key.UserName, data.Key.Donate.ToString());
            playerView.transform.localScale = Vector3.one;
        }
    }

    public void AddPlayer(User user, Sprite avatar)
    {
        var playerView = Instantiate(_playerCellPrefab, _parent);
        playerView.Initialize(avatar, name, user.Donate.ToString());
        playerView.transform.localScale = Vector3.one;
    }
}
