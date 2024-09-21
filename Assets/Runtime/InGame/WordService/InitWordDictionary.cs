using System;
using Runtime.Vocabulary;
using UnityEngine;

namespace Runtime.InGame.WordService
{
    public class InitWordDictionary : MonoBehaviour
    {
        private void Awake()
        {
            WordDictionary.Init();
        }
    }
}