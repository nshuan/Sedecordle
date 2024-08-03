using System;
using Runtime.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Home
{
    public class HomeBackground : MonoBehaviour
    {
        [SerializeField] private Image bgImage;
        
        private void Awake()
        {
            bgImage.SetColor(ColorConst.Default.backgroundColor);
        }
    }
}