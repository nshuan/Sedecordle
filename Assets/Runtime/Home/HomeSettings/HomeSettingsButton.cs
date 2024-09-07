using Core.PopUp;
using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.Home.HomeSettings
{
    public class HomeSettingsButton : MonoBehaviour, IPointerClickHandler, IAffectedByDarkMode
    {
        [SerializeField] private Image buttonIcon;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PopUp.Show(PopUp.Get<HomeSettingsPopUp>());
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return buttonIcon.DOColor(colorPalette.iconColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}