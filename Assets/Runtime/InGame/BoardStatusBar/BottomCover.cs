using System;
using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.BoardStatusBar
{
    public class BottomCover : MonoBehaviour, IAffectedByDarkMode
    {
        [SerializeField] private Image cover;

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return cover.DOColor(ColorConst.Default.backgroundColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}