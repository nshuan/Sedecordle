using System;
using Runtime.InGame.Keyboard;
using UnityEngine;

namespace Runtime.InGame
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnEnterPressed;
        public static event Action OnBackspacePressed;
        public static event Action<KeyCode> OnLetterPressed;

        private static bool Inputting { get; set; } = true;
        
        private void Awake()
        {
            KeyboardKey.OnVirtualKeyPressed += OnVirtualKeyPressed;
            UnBlockInput();
        }

        private void OnDestroy()
        {
            KeyboardKey.OnVirtualKeyPressed -= OnVirtualKeyPressed;
        }

        private void OnGUI()
        {
            if (!Inputting) return;
            
            var e = Event.current;
            
            if (e.type == EventType.KeyDown)
            {
                var key = e.keyCode;
                if (key == KeyCode.Return)
                {
                    OnEnterPressed?.Invoke();
                } 
                if (key == KeyCode.Backspace)
                {
                    OnBackspacePressed?.Invoke();
                }
                else if (key.ToString().Length == 1 && char.IsLetter(key.ToString()[0]))
                {
                    OnLetterPressed?.Invoke(key);
                }
            }
        }

        private void OnVirtualKeyPressed(KeyCode key)
        {
            if (!Inputting) return;
            
            if (key == KeyCode.Return)
            {
                OnEnterPressed?.Invoke();
            } 
            if (key == KeyCode.Backspace)
            {
                OnBackspacePressed?.Invoke();
            }
            else if (key.ToString().Length == 1 && char.IsLetter(key.ToString()[0]))
            {
                OnLetterPressed?.Invoke(key);
            }
        }

        public static void BlockInput()
        {
            Inputting = false;
        }

        public static void UnBlockInput()
        {
            Inputting = true;
        }
    }
}