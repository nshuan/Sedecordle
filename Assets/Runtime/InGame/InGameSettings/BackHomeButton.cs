using Core.PopUp;
using Runtime.InGame.ConfirmPopUp;
using Runtime.LoadingEffect;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.InGame.InGameSettings
{
    public class BackHomeButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            var confirmPopUp = PopUp.Get<AreYouSurePopUp>();
            confirmPopUp.onYesAction += () =>
            {
                PopUp.Hide(confirmPopUp);
                Loading.Show(2f);
                Navigator.LoadScene("Home");
            };
            confirmPopUp.onNoAction += () =>
            {
                PopUp.Hide(confirmPopUp);
            };

            PopUp.Show(confirmPopUp);
        }
    }
}