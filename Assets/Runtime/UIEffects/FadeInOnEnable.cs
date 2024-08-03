using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UIEffects
{
    public class FadeInOnEnable : MonoBehaviour
    {
        [SerializeField] private float duration = 0.15f;
        [SerializeField] private Ease easing = Ease.OutCubic;
        
        private Graphic[] _graphicsChildren;
        
        private void Awake()
        {
            _graphicsChildren = GetComponentsInChildren<Graphic>();
        }

        private void OnEnable()
        {
            DoFadeIn();
        }

        private Tween DoFadeIn()
        {
            var seq = DOTween.Sequence(transform);

            foreach (var graphic in _graphicsChildren)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0.2f);

                seq.Join(graphic.DOFade(1f, duration).SetEase(easing));
            }

            return seq;
        }
    }
}