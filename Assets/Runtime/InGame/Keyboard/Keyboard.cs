using System;
using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using Runtime.InGame.Board;
using UnityEngine;

namespace Runtime.InGame.Keyboard
{
    public class Keyboard : MonoBehaviour
    {
        private List<KeyboardKey> _keys;

        private void Awake()
        {
            _keys = GetComponentsInChildren<KeyboardKey>().ToList();
        }
        
        [Button]
        private void Start()
        {
            var size = BoardManager.BoardContainerSize;
            foreach (var key in _keys)
            {
                key.SetupKey(size.x, size.y);
            }
        }
    }
}