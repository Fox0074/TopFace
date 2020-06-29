using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerCell : MonoBehaviour
{
    [SerializeField]
    private Image _avatarImage;

    [SerializeField]
    private TextMeshProUGUI _playerName;

    public void Initialize(Sprite avatarSprite, string playerName)
    {
        _avatarImage.sprite = avatarSprite;
        _playerName.text = playerName;
    }
}
