using System.Collections.Generic;
using Core.Singleton;
using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.ToastNotify
{
    public class ToastItem : MonoBehaviour
    {
        [SerializeField] private Image toastImage;
        [SerializeField] private Text toastText;
        [SerializeField] private Image toastImageView;
        [SerializeField] private Text toastTextView;
        [SerializeField] private Vector2 toastFrom;
        [SerializeField] private Vector2 toastTo;
        [SerializeField] private float alphaFrom;
        [SerializeField] private float alphaTo;
        [SerializeField] private float duration;
        
        protected void Awake()
        {
            toastImage.gameObject.SetActive(false);
        }

        public Tween Show(string message)
        {
            toastImageView.color = new Color(1f, 0.6084906f, 0.6084906f, 1f);
            toastTextView.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1f);
            return ShowText(message);
        }

        public Tween Show(string message, Color color)
        {
            toastImageView.color = color;
            toastTextView.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1f);
            return ShowText(message);
        }
        
        public Tween Show(string message, Color color, Color textColor)
        {
            toastImageView.color = color;
            toastTextView.color = textColor;
            return ShowText(message);
        }

        [Button]
        public Tween ShowText(string message)
        {
            toastText.text = message;
            toastTextView.text = message;
            
            DOTween.Kill(transform);
            return DoShow();
        }
        
        private Tween DoShow()
        {
            transform.localPosition = toastFrom;
            transform.localScale = 0.3f * Vector3.one;
            toastImageView.color = new Color(toastImageView.color.r, toastImageView.color.g, toastImageView.color.b, alphaFrom);
            toastTextView.color = new Color(toastTextView.color.r, toastTextView.color.g, toastTextView.color.b, alphaFrom);
            
            var sequence = DOTween.Sequence(transform);
            sequence.AppendCallback(() => toastImage.gameObject.SetActive(true));
            sequence.Join(transform.DOScale(1f, 0.3f));
            sequence.Join(transform.DOLocalMoveY(toastTo.y, duration).SetEase(Ease.OutExpo))
                .Join(toastTextView.DOFade(alphaTo, duration).SetEase(Ease.OutExpo))
                .Join(toastImageView.DOFade(alphaTo, duration).SetEase(Ease.OutExpo))
                .Append(toastTextView.DOFade(0f, 0.2f))
                .Join(toastImageView.DOFade(0f, 0.2f));
            sequence.OnComplete(() => toastImage.gameObject.SetActive(false));
            return sequence;
        }
    }
}