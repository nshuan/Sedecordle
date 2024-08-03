using Runtime.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame
{
    public class InGameBackground : MonoBehaviour
    {
        [SerializeField] private Image bgImage;
        
        private void Awake()
        {
            bgImage.SetColor(ColorConst.Default.backgroundColor);
        }
    }
}