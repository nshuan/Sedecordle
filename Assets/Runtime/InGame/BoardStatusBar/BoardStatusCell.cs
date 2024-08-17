using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.BoardStatusBar
{
    public class BoardStatusCell : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        public Image CellImage
        {
            get => image;
            set => image = value;
        }

        public Button CellButton
        {
            get => button;
            set => button = value;
        }

        public int CellBoardIndex
        {
            get;
            set;
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }
    }
}