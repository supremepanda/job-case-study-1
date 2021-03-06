using System.Collections;
using Common;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level2
{
    public class SpawnController : MonoBehaviour, ISpawnOlive
    {
        [SerializeField] private GameObject olive;
        [SerializeField] private Transform[] spawnPositions;

        [SerializeField] private GameObject[] foods;

        private Progress _progress;

        private bool _canSpawn = true;
        
        /// <summary>
        /// Spawn olives on pre-defined and shuffled spawn positions.
        /// </summary>
        /// <param name="amount">olive amount</param>
        public void SpawnOlive(int amount)
        {
            spawnPositions = Shuffle(spawnPositions);
            
            for(int ind = 0; ind < amount; ind++)
            {
                Instantiate(olive, spawnPositions[ind].position, Quaternion.identity);
            }
        }
        
        private Transform[] Shuffle(Transform[] values)
        {
            for (int t = 0; t < values.Length; t++ )
            {
                Transform tmp = values[t];
                int r = Random.Range(t, values.Length);
                values[t] = values[r];
                values[r] = tmp;
            }

            return values;
        }
        /// <summary>
        /// Spawn next food function. 
        /// </summary>
        public void SpawnFood(int index, Vector3 spawnPos, FoodType foodType, GameObject spawner, GameObject collidedObject)
        {
            if (_canSpawn)
            {
                _canSpawn = false;
                var food = Instantiate(foods[index], spawnPos, Quaternion.identity).GetComponent<Food>().foodType =
                    foodType;
                
                // Handle calling SpawnFood function twice problem using _canSpawn variable.
                StartCoroutine(ActiveCanSpawn());
                
                _progress.IncreaseProgress();
                
                //Destroy two collided object.
                Destroy(spawner);
                Destroy(collidedObject);
            }
        }

        private IEnumerator ActiveCanSpawn()
        {
            yield return new WaitForSeconds(0.001f);
            _canSpawn = true;
        }

        private void Start()
        {
            _progress = FindObjectOfType<Progress>();
            SpawnOlive(Level2Constants.OliveAmount);
        }
    }
}