using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.SimpleListView
{
    public class CellCreator : MonoBehaviour
    {
        [SerializeField] private RectTransform[] cellPrefabs;

        private GameObject goCellPool;

        private Dictionary<Type, Queue<SimpleCell.ICellView>> cellPools;
        private Dictionary<Type, (RectTransform prefab, Vector2 size)> cells;

        private void Awake()
        {
            cellPools = new Dictionary<Type, Queue<SimpleCell.ICellView>>();
            cells = new Dictionary<Type, (RectTransform prefab, Vector2 size)>();

            foreach (var t in cellPrefabs)
            {
                var layOutElement = t.GetComponent<LayoutElement>();
                var view = t.GetComponent<SimpleCell.ICellView>().GetType();
                cellPools.Add(view, new Queue<SimpleCell.ICellView>());
                cells.Add(view, (t, new Vector2(layOutElement.minWidth, layOutElement.minHeight)));
            }

            goCellPool = new GameObject(">------> cell pool <-------<", typeof(RectTransform));
            goCellPool.transform.SetParent(transform, false);
        }

        public SimpleCell.ICellView Get(SimpleCell.ICellData cellData)
        {
            var type = cellData.ViewType;
            if (cellPools[type].Count == 0)
            {
                var cellRect = Instantiate(cells[type].prefab, goCellPool.transform);
                cellRect.gameObject.SetActive(true);
                return cellRect.gameObject.GetComponent<SimpleCell.ICellView>();
            }

            var cellView = cellPools[type].Dequeue();
            ((Component)cellView).gameObject.SetActive(true);
            return cellView;
        }

        public Vector2 CellSize(SimpleCell.ICellData cellData)
        {
            return cells[cellData.ViewType].size;
        }

        public void Release(SimpleCell.ICellData cellData, SimpleCell.ICellView cellView)
        {
            var cellTransform = ((Component)cellView).transform;
            cellTransform.SetParent(goCellPool.transform, false);
            cellTransform.gameObject.SetActive(false);
            cellPools[cellData.ViewType].Enqueue(cellView);
        }
    }
}