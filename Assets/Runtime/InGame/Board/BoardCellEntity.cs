using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Board
{
    public class BoardCellEntity : MonoBehaviour
    {
        [SerializeField] private Image cellImage;
        [SerializeField] private Text cellText;

        public Image CellImage => cellImage;
        public Text CellText => cellText;
        
        public void InitCell()
        {
            cellText.text = "";
        }

        public void Write(KeyCode key)
        {
            cellText.text = key.ToString();
            DoWrite();
        }

        public void Delete()
        {
            cellText.text = "";
        }

        private Tween DoWrite()
        {
            DOTween.Kill(transform);
            var seq = DOTween.Sequence(transform);

            seq.AppendCallback(() => cellText.transform.localScale = 0.5f * Vector3.one);
            seq.Append(cellText.transform.DOScale(1f, 0.1f))
                .Append(cellImage.transform.DOScale(1.1f, 0.1f))
                .Append(cellImage.transform.DOScale(1f, 0.06f));

            return seq;
        }
    }
}