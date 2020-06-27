using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersFactory : MonoBehaviour
{
    [SerializeField]
    private PlayerCell _playerCellPrefab; 

    [SerializeField]
    private Transform _parent; 


    public void Initialized(Dictionary<Sprite,string> playersData)
    {
        foreach (var data in playersData)
        {
            var playerView = Instantiate(_playerCellPrefab, _parent);
            playerView.transform.localScale = Vector3.one;
        }
    }
}
