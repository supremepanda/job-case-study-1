using Common;
using UnityEngine;

namespace Level3
{
    public class FoodSpawner : MonoBehaviour
    {
        public Food[,] FoodGameArea = new Food[6, 5];
        
        [Tooltip("Three food types.")]
        [SerializeField] private GameObject[] foods = new GameObject[3];
        
        [Tooltip("Distance between to food.")]
        [SerializeField] private float distanceOfFoods;
        
        [Tooltip("Offset to replace GameArea parent object correctly.")]
        [SerializeField] private Vector3 offset;
        
        [Tooltip("Game area parent object to instantiate foods inside in it.")]
        [SerializeField] private GameObject gameAreaInScene;

        /// <summary>
        /// Spawn foods into game area.
        /// </summary>
        public void SpawnFoods(int[,] gameArea)
        {
            for (int height = 0; height < 6; height++)
            {
                for (int width = 0; width < 5; width++)
                {
                    int ind = gameArea[height, width];
                    var food = Instantiate(foods[ind], new Vector3(
                            width * distanceOfFoods + offset.x, 0f + offset.y, height * -distanceOfFoods + offset.z),
                        Quaternion.identity, gameAreaInScene.transform);
                    FoodGameArea[height, width] = food.GetComponent<Food>();
                    food.GetComponent<Food>().index2DArray = (height, width);
                }
            }
        }
    }
}