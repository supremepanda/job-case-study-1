using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3
{
    public class CandyCrushGameControl : MonoBehaviour
    {
        public delegate void CollectTwoFood(Food food1, Food food2);

        public event CollectTwoFood TwoFoodCollected;
        
        private FoodSelectionManager _foodSelectionManager;
        private List<Food> _foodCouple = new List<Food>();
        private (Vector3, Vector3) _firstPositions;
        
        private void Start()
        {
            _foodSelectionManager = FindObjectOfType<FoodSelectionManager>();
            _foodSelectionManager.OnSelectedFood += AddFood;
        }

        private void AddFood(Food food)
        {
            if (_foodCouple.Count < 2)
            {   
                _foodCouple.Add(food);
                if (_foodCouple.Count == 2)
                {
                    ChangeFoodPositions();
                    if (CheckRules())
                    {
                        TwoFoodCollected?.Invoke(_foodCouple[0], _foodCouple[1]);
                        ClearFoods();
                        // toDO: Action
                    }
                    else
                    {
                        TwoFoodCollected?.Invoke(_foodCouple[0], _foodCouple[1]);
                        StartCoroutine(ReversePositions());
                    }
                }
            }
        }

        private void ClearFoods()
        {
            _foodCouple.RemoveRange(0, 2);
            
        }

        private void ChangeFoodPositions()
        {
            var food1 = _foodCouple[0];
            var food2 = _foodCouple[1];

            var position1 = food1.transform.position;
            var position2 = food2.transform.position;
            
            _firstPositions = (position1, position2);

            position1 = Vector3.Lerp(position1, _firstPositions.Item2, 1);
            food1.transform.position = position1;
            
            position2 = Vector3.Lerp(position2, _firstPositions.Item1, 1f);
            food2.transform.position = position2;
        }

        private IEnumerator ReversePositions()
        {
            yield return new WaitForSeconds(1.5f);
            var food1 = _foodCouple[0];
            var food2 = _foodCouple[1];

            food1.transform.position = Vector3.Lerp(food1.transform.position, _firstPositions.Item1, 1f);
            food2.transform.position = Vector3.Lerp(food2.transform.position, _firstPositions.Item2, 1f);
            _firstPositions = (Vector3.zero, Vector3.zero);
            
            ClearFoods();
        }

        private bool CheckRules()
        {
            if (_foodCouple[0].foodType == _foodCouple[1].foodType) return false;
            if (Vector3.Distance(_foodCouple[0].transform.position, _foodCouple[1].transform.position) > 1.5f)
                return false;
            
            // toDo: Eslesme durumunu kontrol et.
            return true;
        }
        
        
    }
}
