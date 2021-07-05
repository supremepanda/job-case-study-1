using UnityEngine;
using Random = UnityEngine.Random;

namespace Level4
{
    public class SpawnFoods : MonoBehaviour
    {
        [SerializeField] private GameObject[] foods = new GameObject[5];
        [SerializeField] private Transform[] spawnPositions = new Transform[25];

        private void SpawnFoodsToPositions()
        {
            foreach (var spawnTransform in spawnPositions)
            {
                int randomFood = Random.Range(0, 5);
                Instantiate(foods[randomFood], spawnTransform.position, Quaternion.identity);
            }
        }
        
        private void Start()
        {
            SpawnFoodsToPositions();
        }
    }
}
