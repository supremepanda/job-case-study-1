using System;
using UnityEngine;

namespace Common
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public Rewards[] rewards = new Rewards[4];
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }
    }
}
