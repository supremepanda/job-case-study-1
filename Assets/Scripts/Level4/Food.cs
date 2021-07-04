using Common;
using UnityEngine;

namespace Level4
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private FoodType foodType;
        public FoodType FoodType => foodType;
    }
}
