using System;
using Runtime.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.BoardStatusBar
{
    public class BottomCover : MonoBehaviour
    {
        [SerializeField] private Image cover;
        
        private void Awake()
        {
            cover.SetColor(ColorConst.Default.backgroundColor);
        }
    }
}