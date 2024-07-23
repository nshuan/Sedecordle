using DG.Tweening;
using Runtime.InGame;
using UnityEngine;
using UnityEngine.EventSystems;
using Runtime.LoadingEffect;

namespace Runtime.Home
{
    public class PlayButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Loading.Show(2f);
            Navigator.LoadScene("InGame");
        }
    }
}