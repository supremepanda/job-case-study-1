using Common;
using UnityEngine;

namespace Level3
{
    public class FoodSpawner : MonoBehaviour
    {
        public Food[,] FoodGameArea = new Food[6, 5];
        
        [SerializeField] private GameObject[] foods = new GameObject[3];
        [SerializeField] private float distanceOfFoods;
        [SerializeField] private Vector3 offset;
        [SerializeField] private GameObject gameAreaInScene;

        public void SpawnFoods(int[,] gameArea)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int ind = gameArea[i, j];
                    var food = Instantiate(foods[ind], new Vector3(
                            i * distanceOfFoods + offset.x, 0f + offset.y, j * distanceOfFoods + offset.z),
                        Quaternion.identity, gameAreaInScene.transform);
                    FoodGameArea[i, j] = food.GetComponent<Food>();
                }
            }
        }
    }
}