using System;
using Runtime.Const;
using UnityEngine;

namespace Runtime.InGame.InGameSettings
{
    public class SwitchColorMode : MonoBehaviour
    {
        public static event Action onSwitchColorMode;
        
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