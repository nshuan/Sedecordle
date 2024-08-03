using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame
{
    public class InGameBackground : MonoBehaviour, IAffectedByDarkMode
    {
        [SerializeField] private Image bgImage;

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return bgImage.DOColor(ColorConst.Default.backgroundColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}