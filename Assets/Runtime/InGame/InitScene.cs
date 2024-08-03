using System;
using Runtime.LoadingEffect;
using UnityEngine;

namespace Runtime.InGame
{
    public class InitScene : MonoBehaviour
    {
        private void Start()
        {
            Loading.Show(2f);
            Navigator.LoadScene("Home");
        }
    }
}