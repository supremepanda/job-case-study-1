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
            for (int height = 0; height < 6; height++)
            {
                for (int width = 0; width < 5; width++)
                {
                    int ind = gameArea[height, width];
                    var food = Instantiate(foods[ind], new Vector3(
                            height * distanceOfFoods + offset.x, 0f + offset.y, width * distanceOfFoods + offset.z),
                        Quaternion.identity, gameAreaInScene.transform);
                    FoodGameArea[height, width] = food.GetComponent<Food>();
                    food.GetComponent<Food>().index2DArray = (height, width);
                }
            }
        }
    }
}