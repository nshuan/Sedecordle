using Core.PopUp;
using EasyButtons;
using UnityEngine;

namespace Runtime.InGame.Test
{
    public class ShowTestPopUp : MonoBehaviour
    {
        [Button]
        private void ShowPopUp()
        {
            var popUp = PopUp.Get<TestPopUp>();
            PopUp.Show(popUp);
        }

        [Button]
        private void HidePopUp()
        {
            var popUp = PopUp.Get<TestPopUp>();
            PopUp.Hide(popUp);
        }
    }
}