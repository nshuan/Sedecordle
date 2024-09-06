using Runtime.LoadingEffect;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.InGame.WinLose
{
    public class ReplayButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Loading.Show(2f);
            Navigator.LoadScene("InGame");
        }
    }
}