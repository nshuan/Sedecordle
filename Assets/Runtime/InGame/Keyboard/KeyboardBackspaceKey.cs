using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Keyboard
{
    public class KeyboardBackspaceKey : KeyboardKey
    {
        [SerializeField] private Image backspaceIcon;
        
        public override Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return DOTween.Sequence(transform)
                .Join(backspaceIcon.DOColor(colorPalette.keyboardLetterColor, 0.2f).SetEase(Ease.OutQuint))
                .Join(letterImage.DOColor(colorPalette.keyboardKeyColor, 0.2f).SetEase(Ease.OutQuint));
        }
    }
}