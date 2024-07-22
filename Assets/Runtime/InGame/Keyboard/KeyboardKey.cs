using System;
using Runtime.Const;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.InGame.Keyboard
{
    public class KeyboardKey : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<KeyCode> OnVirtualKeyPressed;

        [SerializeField] private Text letterText;
        [SerializeField] private KeyCode letter;

        private void Awake()
        {
            letterText.text = letter.ToString();
            letterText.color = ColorConst.Default.keyboardLetterColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnVirtualKeyPressed?.Invoke(letter);
        }
    }
}