using System;
using Core.PopUp;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.ConfirmPopUp
{
    public class AreYouSurePopUp : MonoBehaviour, IPopUp
    {
        [SerializeField] private Button yesButton, noButton;
        
        public Action onYesAction;
        public Action onNoAction;

        private void OnEnable()
        {
            yesButton.onClick.AddListener(() => onYesAction?.Invoke());
            noButton.onClick.AddListener(() => onNoAction?.Invoke());
        }

        private void OnDisable()
        {
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        }
    }
}