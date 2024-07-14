using UnityEngine;

namespace Core.SimpleListView
{
    public class Descending : SimpleListView.Order
    {
        public override int Mul => -1;

        public override void SetHeaderSibling()
        {
            header.SetAsLastSibling();
        }

        public override void SetFooterSibling()
        {
            footer.SetAsFirstSibling();
        }

        public override void SetSibling(Transform transform, int index, int count)
        {
            transform.SetSiblingIndex(count - index - 2);
        }

        public override float CalculateVal(float val) => val;
        public override float CalculateCenterPoint(float axis) => +axis;
    }
}