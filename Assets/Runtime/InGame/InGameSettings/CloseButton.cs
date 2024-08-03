using System;
using Core.PopUp;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.InGame.InGameSettings
{
    public class CloseButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject popUp;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PopUp.Hide(popUp);
        }
    }
}