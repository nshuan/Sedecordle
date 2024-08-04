using System;
using Runtime.Const;
using Runtime.UIButtons;
using UnityEngine;

namespace Runtime.InGame.InGameSettings
{
    public class SwitchColorMode : MonoBehaviour
    {
        [SerializeField] private CheckBox checkBox;
        
        public static event Action onSwitchColorMode;

        private void OnEnable()
        {
            checkBox.SetCheck(GameSettings.Load.colorMode == ColorMode.Dark);
        }

        public void DarkModeOn()
        {
            GameSettings.Load.colorMode = ColorMode.Dark;
            GameSettings.Save();
            onSwitchColorMode?.Invoke();
        }

        public void DarkModeOff()
        {
            GameSettings.Load.colorMode = ColorMode.Light;
            GameSettings.Save();
            onSwitchColorMode?.Invoke();
        }
    }
}