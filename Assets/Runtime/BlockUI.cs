using System;
using Core.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime
{
    [RequireComponent(typeof(Canvas))]
    public class BlockUI : MonoSingleton<BlockUI>
    {
        [SerializeField] private Image cover;

        private Canvas _canvas;
        
        protected override void Awake()
        {
            base.Awake();

            _canvas = GetComponent<Canvas>();
            _canvas.sortingOrder = 1000;
            cover.color += new Color(0f, 0f, 0f, -1f);
        }

        public void Block()
        {
            cover.gameObject.SetActive(true);
        }

        public void UnBlock()
        {
            cover.gameObject.SetActive(false);
        }
    }
}