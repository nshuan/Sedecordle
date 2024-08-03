using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Const;
using Runtime.InGame.InGameSettings;
using UnityEngine;

namespace Runtime.DarkMode
{
    public class ColorEffector : MonoBehaviour
    {
        [SerializeField] private bool changeImmediately;

        private List<IAffectedByDarkMode> _graphics;
        
        private void Awake()
        {
            _graphics = new List<IAffectedByDarkMode>();
            var objects = FindObjectsOfType<MonoBehaviour>(true);
            foreach (var obj in objects)
            {
                if (obj is IAffectedByDarkMode bearer) _graphics.Add(bearer);
            }

            SwitchColorMode.onSwitchColorMode += OnSwitchColorMode;
        }

        private void OnEnable()
        {
            DoChangeColor();
        }

        private void OnDestroy()
        {
            SwitchColorMode.onSwitchColorMode -= OnSwitchColorMode;
        }

        private void OnSwitchColorMode()
        {
            DoChangeColor();
        }
        
        private Tween DoChangeColor()
        {
            DOTween.Kill(transform);
            var seq = DOTween.Sequence();
            var colorPalette = ColorConst.Reload;
            
            foreach (var graphic in _graphics)
            {
                seq.Join(graphic.DoChangeColorMode(colorPalette));
            }

            return seq;
        }
    }
}