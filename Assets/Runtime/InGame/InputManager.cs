using System;
using UnityEngine;

namespace Runtime.InGame
{
    public class InputManager : MonoBehaviour
    {
        public static event Action<KeyCode> OnLetterPressed;
        
        private void OnGUI()
        {
            var e = Event.current;

            if (e.type == EventType.KeyDown)
            {
                var key = e.keyCode;
                if (key.ToString().Length == 1 && char.IsLetter(key.ToString()[0]))
                {
                    OnLetterPressed?.Invoke(key);
                }
            }
        }
    }
}