using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Const;
using Runtime.InGame.Board;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.BoardStatusBar
{
    public class BoardStatusPreviewBar : MonoBehaviour
    {
        [SerializeField] private Transform cellHolder;
        private List<BoardStatusCell> _boardStatusCells;
        private HorizontalLayoutGroup _horizontalLayoutGroup;
        
        private void Awake()
        {
            _boardStatusCells = GetComponentsInChildren<BoardStatusCell>().ToList();
            if (!TryGetComponent<HorizontalLayoutGroup>(out _horizontalLayoutGroup))
                _horizontalLayoutGroup = gameObject.AddComponent<HorizontalLayoutGroup>();
            _horizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            _horizontalLayoutGroup.childControlHeight = true;
            _horizontalLayoutGroup.childControlWidth = true;
            _horizontalLayoutGroup.childForceExpandHeight = true;
            _horizontalLayoutGroup.childForceExpandWidth = true;
            
            GameManager.OnLoadGame += SetupBar;
        }

        private void SetupBar()
        {
            for (var i = GameManager.Instance.BoardEntities.Count; i < _boardStatusCells.Count; i++)
            {
                _boardStatusCells[i].gameObject.SetActive(false);
            }

            for (var i = _boardStatusCells.Count; i < GameManager.Instance.BoardEntities.Count; i++)
            {
                var newCell = new GameObject("BoardStatusCell");
                newCell.transform.SetParent(cellHolder);
                newCell.transform.localScale = Vector3.one;
                var newCellImage = newCell.AddComponent<Image>();
                var newCellButton = newCell.AddComponent<Button>();
                var newCellScript = newCell.AddComponent<BoardStatusCell>();
                newCellScript.CellImage = newCellImage;
                newCellScript.CellButton = newCellButton;
                newCellScript.SetColor(Color.clear);
                _boardStatusCells.Add(newCellScript);
            }

            SetupEvent();
        }

        private void SetupEvent()
        {
            var amount = BoardManager.BoardAmount;
            for (var i = 0; i < amount; i++)
            {
                if (i > _boardStatusCells.Count) break;

                var cell = _boardStatusCells[i];
                cell.CellBoardIndex = i;
                cell.CellButton.onClick.AddListener(() =>
                {
                    GameManager.Instance.BoardManagerInstance.ScrollToBoard(cell.CellBoardIndex);
                });
                Action onBoardComplete = () =>
                {
                    cell.SetColor(ColorConst.Default.correctColor);
                };

                if (!BoardEntity.OnBoardCompletes.TryAdd(i, onBoardComplete))
                    BoardEntity.OnBoardCompletes[i] += onBoardComplete;
            }
        }
    }
}