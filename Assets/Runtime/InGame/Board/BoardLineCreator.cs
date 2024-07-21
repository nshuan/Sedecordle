using Runtime.Const;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class BoardLineCreator : MonoBehaviour
    {
        [SerializeField] private BoardLineEntity lineEntity;

        private const int LineAmount = 21;
        
        public BoardLineEntity[] CreateLines(int numberOfLetter)
        {
            var lineEntities = new BoardLineEntity[LineAmount];
            
            for (var i = 0; i < LineAmount; i++)
            {
                var line = Instantiate(lineEntity, transform);
                line.InitBoardLine(numberOfLetter);
                line.LineColor = ColorConst.Default.pendingLineColor;
                lineEntities[i] = line;
            }

            lineEntities[0].LineColor = ColorConst.Default.activeLineColor;

            return lineEntities;
        }
    }
}