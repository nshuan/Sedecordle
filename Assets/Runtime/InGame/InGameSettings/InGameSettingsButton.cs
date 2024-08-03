using System;
using Core.PopUp;
using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.InGame.InGameSettings
{
    public class InGameSettingsButton : MonoBehaviour, IPointerClickHandler, IAffectedByDarkMode
    {
        [SerializeField] private Image buttonIcon;

        public void OnPointerClick(PointerEventData eventData)
        {
            PopUp.Show(PopUp.Get<InGameSettingsPopUp>());
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return buttonIcon.DOColor(colorPalette.iconColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}