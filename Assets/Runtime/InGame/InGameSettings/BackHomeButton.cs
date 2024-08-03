using Runtime.LoadingEffect;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.InGame.InGameSettings
{
    public class BackHomeButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Loading.Show(2f);
            Navigator.LoadScene("Home");
        }
    }
}