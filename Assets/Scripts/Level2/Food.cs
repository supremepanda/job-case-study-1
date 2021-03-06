using Common;
using UnityEngine;

namespace Level2
{
    public class Food : MonoBehaviour
    {
        public FoodType foodType;
        private SpawnController _spawnController;
        private Progress _progress;

        private void Start()
        {
            _spawnController = FindObjectOfType<SpawnController>();
            _progress = FindObjectOfType<Progress>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Food")) return;
            if (other.gameObject.GetComponent<Food>().foodType != foodType) return;
            
            if (foodType != FoodType.CheeseSlice)
            {
                // Spawn next food according to collided food type.
                _spawnController.SpawnFood((int) foodType + 1, new Vector3(other.GetContact(0).point.x, 0f, other.GetContact(0).point.z),
                    (FoodType) (int) foodType + 1, gameObject, other.gameObject);
            }
            else
            {
                _progress.IncreaseProgress();
            }
        }
    }
}

