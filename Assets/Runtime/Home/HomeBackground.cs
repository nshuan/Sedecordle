using System;
using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Home
{
    public class HomeBackground : MonoBehaviour, IAffectedByDarkMode
    {
        [SerializeField] private Image bgImage;
        
        private void Awake()
        {
            bgImage.SetColor(ColorConst.Default.backgroundColor);
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return bgImage.DOColor(colorPalette.backgroundColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}