using System;
using Runtime.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Home.SelectHardness
{
    public class UIHardnessSelector : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Text title;
        [SerializeField] private Text enter;
        [SerializeField] private Graphic[] textGraphics;

        private void Awake()
        {
            background.SetColor(ColorConst.Default.backgroundColor);
            title.SetColor(ColorConst.Default.activeTextColor);
            enter.SetColor(ColorConst.Default.activeTextColor);
            foreach (var graphic in textGraphics)
            {
                graphic.SetColor(ColorConst.Default.activeTextColor);
            }
        }
    }
}