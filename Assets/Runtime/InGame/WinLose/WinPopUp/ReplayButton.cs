using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.InGame.WinLose
{
    public class ReplayButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Navigator.LoadScene("InGame");
        }
    }
}