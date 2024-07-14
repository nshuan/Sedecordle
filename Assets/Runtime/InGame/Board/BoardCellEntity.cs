using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Board
{
    public class BoardCellEntity : MonoBehaviour
    {
        [SerializeField] private Text cellText;
        
        public void InitCell()
        {
            cellText.text = "";
        }

        public void Write(KeyCode key)
        {
            cellText.text = key.ToString();
        }

        public void Delete()
        {
            cellText.text = "";
        }
    }
}