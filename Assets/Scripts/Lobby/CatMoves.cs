using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace FizerFox.Meta
{
    public class CatMoves : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Transform _catTransform;

        [SerializeField]
        private Image _room;

        public void OnPointerClick(PointerEventData eventData)
        {
            _catTransform.DOMove(eventData.position, 1f);
        }
    }
}