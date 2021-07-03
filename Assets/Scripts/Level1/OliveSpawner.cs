using System;
using Common;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level1
{
    public class OliveSpawner : MonoBehaviour, ISpawnOlive
    {
        [SerializeField] private GameObject olive;

        public void SpawnOlive(int amount)
        {
            for (int ind = 0; ind < amount; ind++)
            {
                float xSpawnPos = Random.Range(-OliveSpawnRange.X, OliveSpawnRange.X);
                float zSpawnPos = Random.Range(OliveSpawnRange.NegativeZ, OliveSpawnRange.PositiveZ);
                Vector3 spawnVector = new Vector3(xSpawnPos, OliveSpawnRange.Y, zSpawnPos);
                
                Instantiate(olive, spawnVector, Quaternion.identity);
            }
        }

        private void Start()
        {
            SpawnOlive(Level1Constants.OliveAmount);
        }
    }
    
}
