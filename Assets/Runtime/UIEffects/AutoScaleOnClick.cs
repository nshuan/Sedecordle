using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.UIEffects
{
    public class AutoScaleOnClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Vector2 scaleOnClick = 0.8f * Vector2.one;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(scaleOnClick, 0.1f).SetEase(Ease.InSine);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(1f, 0.1f).SetEase(Ease.OutSine);
        }
    }
}