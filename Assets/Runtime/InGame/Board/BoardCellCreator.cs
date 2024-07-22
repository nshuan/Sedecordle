using System;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using Runtime.Const;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class BoardCellCreator : MonoBehaviour
    {
        [SerializeField] private BoardCellEntity cellEntity;

        private RectTransform _rectTransform;
        private HorizontalLayoutGroup _layoutGroup;

        [SerializeField] private Vector2Int padding;
        [SerializeField] private float spacing;
        
        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _layoutGroup = GetComponent<HorizontalLayoutGroup>();
            
            _rectTransform.pivot = new Vector2(0.5f, 1f);
        }

        public BoardCellEntity[] CreateCells(int amount)
        {
            var cellEntities = new BoardCellEntity[amount];

            _layoutGroup.padding = new RectOffset(padding.x, padding.x, padding.y, padding.y);
            _layoutGroup.spacing = spacing;
            _layoutGroup.childControlWidth = true;
            _layoutGroup.childControlHeight = true;
            _layoutGroup.childForceExpandWidth = true;
            _layoutGroup.childForceExpandHeight = true;

            for (var i = 0; i < amount; i++)
            {
                var cell = Instantiate(cellEntity, transform);
                cell.InitCell();
                cell.CellImage.color = ColorConst.Default.pendingLineColor;
                cell.CellText.color = ColorConst.Default.activeTextColor;
                cellEntities[i] = cell;
            }
            
            return cellEntities;
        }

        private void SetupLine(Vector2 size)
        {
            _rectTransform.sizeDelta = size;
        }
        
#if UNITY_EDITOR
        [Button]
        private void TestInitCells(int amount)
        {
            CreateCells(amount);
        }
#endif
    }
}