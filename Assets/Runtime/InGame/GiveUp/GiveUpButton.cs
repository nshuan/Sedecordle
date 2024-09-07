using Core.PopUp;
using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using Runtime.InGame.ConfirmPopUp;
using Runtime.LoadingEffect;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.InGame.GiveUp
{
    public class GiveUpButton : MonoBehaviour, IPointerClickHandler, IAffectedByDarkMode
    {
        [SerializeField] private Image buttonIcon;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var confirmPopUp = PopUp.Get<AreYouSurePopUp>();
            confirmPopUp.onYesAction += () =>
            {
                Loading.Show(2f);
                Navigator.LoadScene("Home");
            };
            confirmPopUp.onNoAction += () =>
            {
                PopUp.Hide(confirmPopUp);
            };

            PopUp.Show(confirmPopUp);
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return buttonIcon.DOColor(colorPalette.iconColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}