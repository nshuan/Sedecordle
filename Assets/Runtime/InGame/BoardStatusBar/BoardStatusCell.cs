using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.BoardStatusBar
{
    public class BoardStatusCell : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public void SetColor(Color color)
        {
            image.color = color;
        }

        public void SetImage(Image image)
        {
            this.image = image;
        }
    }
}