using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class BoardLineCreator : MonoBehaviour
    {
        [SerializeField] private BoardLineEntity lineEntity;
        
        public BoardLineEntity[] CreateLines(int amount)
        {
            var lineEntities = new BoardLineEntity[amount];
            
            for (var i = 0; i < amount; i++)
            {
                var line = Instantiate(lineEntity, transform);
                line.InitBoardLine(5);
                lineEntities[i] = line;
            }

            return lineEntities;
        }
    }
}