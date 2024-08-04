using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Keyboard
{
    public class KeyboardEnterKey : KeyboardKey
    {
        [SerializeField] private Text enterText;
        
        public override Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return DOTween.Sequence(transform)
                .Join(enterText.DOColor(colorPalette.keyboardLetterColor, 0.2f).SetEase(Ease.OutQuint))
                .Join(letterImage.DOColor(colorPalette.keyboardKeyColor, 0.2f).SetEase(Ease.OutQuint));
        }
    }
}