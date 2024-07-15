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

        private Vector2Int _padding = Vector2Int.zero;
        private float _spacing = 0f;
        
        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _layoutGroup = GetComponent<HorizontalLayoutGroup>();
            
            _rectTransform.pivot = new Vector2(0.5f, 1f);
        }

        public BoardCellEntity[] CreateCells(int amount, Vector2Int padding, float spacing = 0f)
        {
            var cellEntities = new BoardCellEntity[amount];
            
            _padding = padding;
            _spacing = spacing;
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
        private void TestInitCells(int amount, int paddingX, int paddingY, float spacing)
        {
            CreateCells(amount, new Vector2Int(paddingX, paddingY), spacing);
        }
#endif
    }
}