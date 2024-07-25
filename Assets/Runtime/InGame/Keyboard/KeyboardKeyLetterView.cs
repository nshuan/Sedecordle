using System;
using Runtime.Const;
using Runtime.InGame.WordChecking;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Keyboard
{
    public class KeyboardKeyLetterView : MonoBehaviour
    {
        private CharMatch _currentValue;
        private Image _image;

        public CharMatch CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                _image.color = _currentValue switch
                {
                    CharMatch.NotExist => ColorConst.Default.notExistColor,
                    CharMatch.NotPlace => ColorConst.Default.notPlaceColor,
                    CharMatch.Correct => ColorConst.Default.correctColor,
                    _ => ColorConst.Default.notExistColor
                };
            }
        }

        public void SetImage(Image component)
        {
            _image = component;
        }
    }
}