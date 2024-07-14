
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Core.SimpleListView
{
    [RequireComponent(typeof(ScrollRect), typeof(CellCreator))]
    public class SimpleListView : MonoBehaviour
    {
        public List<SimpleCell.ICellData> data = new();
        [SerializeField] private Layout layout = new Horizontal();
        [SerializeField] private Order order = new Ascending();
        [SerializeField] private float spacing;
        [SerializeField] private bool alignCenterIfFitContent;
        [SerializeField] private Vector2 size;
        

        [SerializeField] private RectOffset padding = new RectOffset();
        private ScrollRect scrollRect;
        private CellCreator cellCreator;
        private RectTransform tContent;
        private LayoutElement leHeader, leFooter;
        private SimpleCell[] cells;
        
        [ContextMenu("GetSize")]
        private void GetSize() {
            var rect = GetComponent<RectTransform>().rect;
            size = new Vector2(rect.width, rect.height);
            
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets(); 
#endif
        }
        
        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            cellCreator = GetComponent<CellCreator>();
            
            scrollRect.viewport = GetComponent<RectTransform>();
            tContent = new GameObject(">-------content---------<", typeof(RectTransform)).GetComponent<RectTransform>();
            tContent.SetParent(scrollRect.viewport, false);
            scrollRect.content = tContent;

            leHeader = new GameObject(">--------Header--------<", typeof(RectTransform), typeof(LayoutElement)).GetComponent<LayoutElement>();
            leFooter = new GameObject(">--------Footer--------<", typeof(RectTransform), typeof(LayoutElement)).GetComponent<LayoutElement>();

            leHeader.transform.SetParent(tContent, false);
            leFooter.transform.SetParent(tContent, false);

            layout.Set(scrollRect);
            layout.Awake(tContent, padding, scrollRect.viewport.rect, order, spacing);
            order.Awake(leHeader.GetComponent<RectTransform>(), leFooter.GetComponent<RectTransform>());
            layout.ListViewSize = Mathf.Max(layout.ListViewSize, layout.GetAxis(size));

        }

        private void OnEnable()
        {
            scrollRect.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            scrollRect.onValueChanged.RemoveListener(OnValueChanged);
            DeActive(top, bottom);
            top = bottom = 0;
        }

        private int top, bottom;

        private void OnValueChanged(Vector2 val)
        {
            var pointStart = layout.CalculatePoint(val);

            var pointEnd = pointStart + layout.ListViewSize;
            var start = pointStart < cells[0].head ? 0 : CellAt(0, cells.Length - 1, pointStart);
            var end = pointEnd > cells[^1].tail ? (cells.Length - 1) : CellAt(0, cells.Length - 1, pointEnd);

            var dirty = false;
            if (start != top)
            {
                if (start < top)
                {
                    Active(start, top - 1);
                }
                else
                {
                    DeActive(top, start - 1);
                }

                dirty = true;
                top = start;
            }

            if (end != bottom)
            {
                if (end < bottom)
                {
                    DeActive(end + 1, bottom);
                }
                else
                {
                    Active(bottom + 1, end);
                }

                dirty = true;
                bottom = end;
            }

            if (dirty)
            {
                CorrectSibling();
                CorrectHeader();
                CorrectFooter();
            }
        }

        private void CorrectSibling()
        {
            var count = tContent.childCount;
            for (var i = top; i <= bottom; i++)
                order.SetSibling(((Component)cells[i].view).transform, i - top, count);
        }

        private int CellAt(int topAt, int downAt, float point)
        {
            while (topAt != downAt)
            {
                var middle = (topAt + downAt) / 2;
                if (point < cells[middle].head)
                {
                    downAt = middle - 1;
                }
                else if (point > cells[middle].tail)
                {
                    topAt = middle + 1;
                }
                else
                {
                    return middle;
                }
            }

            return topAt;
        }

        private void DeActive(int begin, int end)
        {
            if (cells == null || cells.Length <= begin) return;

            for (var i = begin; i <= end; i++)
            {
                if (cells[i].view != null)
                {
                    cellCreator.Release(cells[i].data, cells[i].view);
                    cells[i].view = null;
                }
            }
        }

        private void Active(int begin, int end)
        {
            for (var i = begin; i <= end; i++)
            {
                if (cells[i].view == null)
                {
                    cells[i].view = cellCreator.Get(cells[i].data);
                    cells[i].data.Craw(cells[i].view);
                    ((Component)cells[i].view).transform.SetParent(tContent, false);
                }
            }
        }

        public void RemoveDataAt(int id, bool releaseView = true)
        {
            if (id < 0 || id >= data.Count) return;
            if (bottom > id) bottom--;
            if (top > id) top--;
            if(releaseView) cellCreator.Release(cells[id].data, cells[id].view);
            data.RemoveAt(id);
        }

        public void RemoveData<T>(Func<T, bool> predicate, bool releaseView = true)
        {
            if (cells == null || cells.Length == 0) return;
            var id = -1;
            for (var index = 0; index < cells.Length; index++)
            {
                var t1 = cells[index];
                if (t1.data is not T t || !predicate(t)) continue;
                id = index;
                break;
            }
            RemoveDataAt(id, releaseView);
        }

        public void Initialize()
        {
            var listViewSize = layout.ListViewSize;
            if (data.Count == 0) return;
            cells = new SimpleCell[data.Count];

            var contentSize = layout.FirstPadding;

            var cell0Size = layout.GetAxis(cellCreator.CellSize(data[0]));
            // cell 0;
            cells[0] = new SimpleCell
            {
                data = data[0],
                point = contentSize,
                size = cell0Size,
                head = 0,
                tail = contentSize + cell0Size + 0.5f * spacing,
            };

            contentSize += cell0Size;

            if (data.Count == 1) goto last_padding;

            contentSize += spacing;

            // cell 1 - cell n-2
            for (var i = 1; i < data.Count - 1; i++)
            {
                var cellData = data[i];
                var cellSize = layout.GetAxis(cellCreator.CellSize(cellData));

                cells[i] = new SimpleCell
                {
                    data = data[i],
                    point = contentSize,
                    size = cellSize,
                    head = contentSize - 0.5f * spacing,
                    tail = contentSize + cellSize + 0.5f * spacing,
                };

                contentSize = contentSize + cellSize + spacing;
            }

            // cell n - 1
            var cellLastSize = layout.GetAxis(cellCreator.CellSize(data[^1]));
            cells[^1] = new SimpleCell
            {
                data = data[^1],
                point = contentSize,
                size = cellLastSize,
                head = contentSize - 0.5f * spacing,
                tail = contentSize + cellLastSize + layout.LastPadding,
            };

            contentSize += cellLastSize;

            last_padding:
            contentSize += layout.LastPadding;

            top = 0;
            bottom = 0;

            while (bottom < cells.Length && cells[bottom].point < listViewSize)
            {
                bottom++;
            }

            bottom--;

            layout.SetContent(contentSize);

            Active(top, bottom);

            CorrectSibling();
            CorrectHeaderAndFooterWithContentSize(contentSize);
        }

        private void CorrectHeaderAndFooterWithContentSize(float contentSize)
        {
            if (contentSize <= layout.ListViewSize)
            {
                if (alignCenterIfFitContent)
                {
                    tContent.pivot = new Vector2(0.5f, 0.5f);
                    var axis = order.CalculateCenterPoint(layout.ListViewSize / 2) * layout.Mul;
                    tContent.anchoredPosition = layout.MakeVector(Vector2.zero, axis);
                }
            }

            CorrectHeader();
            CorrectFooter();
        }

        private void CorrectHeader()
        {
            if (top <= 0)
            {
                leHeader.gameObject.SetActive(false);
            }
            else
            {
                leHeader.gameObject.SetActive(true);

                layout.SetElement(leHeader, cells[top].point - cells[0].point - spacing);
            }

            order.SetHeaderSibling();
        }

        private void CorrectFooter()
        {
            if (bottom >= cells.Length - 1)
            {
                leFooter.gameObject.SetActive(false);
            }
            else
            {
                leFooter.gameObject.SetActive(true);
                layout.SetElement(leFooter, cells[^1].point - cells[bottom].point - spacing - cells[bottom].size + cells[^1].size);
            }

            order.SetFooterSibling();
        }

        public abstract class Layout
        {
            protected RectOffset padding;
            protected Rect viewport;
            protected float spacing;
            protected Order order;
            protected float contentSize;

            public virtual int Mul => 1;

            protected RectTransform content;

            public float ListViewSize { get; set; }

            private float lvs;
            public float FirstPadding { get; protected set; }
            public float LastPadding { get; protected set; }

            public abstract float GetAxis(Vector2 size);
            public abstract Vector2 MakeVector(Vector2 vector, float axis);
            public abstract void Awake();
            public abstract float CalculatePoint(Vector2 val);
            public abstract void SetElement(LayoutElement layoutElement, float size);

            public virtual void SetContent(float size)
            {
                contentSize = size;
            }

            public void Awake(RectTransform contentVal, RectOffset paddingVal, Rect viewportVal, Order orderVal, float spacingVal)
            {
                this.content = contentVal;
                this.padding = paddingVal;
                this.viewport = viewportVal;
                this.order = orderVal;
                this.spacing = spacingVal;

                Awake();
            }

            public abstract void Set(ScrollRect scrollRect);
        }
        
        public abstract class Order
        {
            protected RectTransform header, footer;

            public virtual int Mul => 1;

            public void Awake(RectTransform headerVal, RectTransform footerVal)
            {
                this.header = headerVal;
                this.footer = footerVal;
            }

            public abstract void SetHeaderSibling();
            public abstract void SetFooterSibling();
            public abstract void SetSibling(Transform transform, int index, int count);

            public abstract float CalculateVal(float val);

            public abstract float CalculateCenterPoint(float axis);
        }

        private Coroutine iePlay;

        public void ScrollTo<T>(Func<T, bool> func, float time, AnimationCurve curve = null)
        {
            if (cells == null || cells.Length == 0) return;
            if (cells[^1].tail <= layout.ListViewSize) return;
            SimpleCell cell = null;
            foreach (var t1 in cells)
            {
                if (t1.data is not T t || !func(t)) continue;
                cell = t1;
                break;
            }

            if (cell == null) return;
            if (iePlay != null) StopCoroutine(iePlay);
            iePlay = StartCoroutine(IEScroll(cell, time, curve));
        }

        public SimpleCell Find<T>(Func<T, bool> predicate) {
            if (cells == null || cells.Length == 0) return null;
            foreach (var t1 in cells)
            {
                if (t1.data is T t && predicate(t)) {
                    return t1;
                }
            }

            return null;
        }

        private IEnumerator IEScroll(SimpleCell cell, float time, AnimationCurve curve)
        {
            var contentSize = layout.GetAxis(tContent.rect.size);
            var startPoint = layout.GetAxis(tContent.anchoredPosition);
            var distance = Mathf.Clamp(cell.head == 0 ? 0 : cell.point, 0, contentSize - layout.ListViewSize);
            var dTime = 0f;
            var delta = distance - order.Mul * layout.Mul * startPoint;
            Debug.Log($"{cell.point}_{distance}_{startPoint}_{delta}_{layout.Mul}_{order.Mul}");
            while (dTime <= time)
            {
                dTime += Time.deltaTime;
                var axis = (curve?.Evaluate(Mathf.Min(dTime / time, 1)) ?? Mathf.Min(dTime / time, 1)) * order.Mul * layout.Mul * delta + startPoint;
                tContent.anchoredPosition = layout.MakeVector(tContent.anchoredPosition, axis);
                yield return null;
            }
        }
    }

    public class SimpleCell
    {
        public ICellData data;
        public ICellView view;
        public float point;
        public float size;
        public float head;
        public float tail;

        public override string ToString()
        {
            return $"{point}_{size}_{head}_{tail}";
        }
        
        public abstract class SimpleCellData<T> : ICellData where T : MonoBehaviour, ICellView
        {
            void ICellData.Craw(ICellView cellView) => Setup((T)cellView);
            Type ICellData.ViewType => typeof(T);
            protected abstract void Setup(T cellView);
        }
        
        public interface ICellData
        {
            void Craw(ICellView cellView);
            Type ViewType { get; }
        }
        public interface ICellView
        {
            
        }
    }
}