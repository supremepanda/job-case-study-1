using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Level3
{
    public class CandyCrushGameControl : MonoBehaviour
    {
        public delegate void CollectTwoFood(Food food1, Food food2);

        public event CollectTwoFood TwoFoodCollected;
        
        private FoodSelectionManager _foodSelectionManager;
        private List<Food> _foodCouple = new List<Food>();
        private (Vector3, Vector3) _firstPositions;

        private FoodSpawner _foodSpawner;
        
        private void Start()
        {
            _foodSelectionManager = FindObjectOfType<FoodSelectionManager>();
            _foodSpawner = FindObjectOfType<FoodSpawner>();
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
            
            _foodSpawner.FoodGameArea[food1.index2DArray.Item1, food1.index2DArray.Item2] = food2;
            _foodSpawner.FoodGameArea[food2.index2DArray.Item1, food2.index2DArray.Item2] = food1;
            
            var tempFood1Index = food1.index2DArray;
            food1.index2DArray = food2.index2DArray;
            food2.index2DArray = tempFood1Index;
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
            if (Vector3.Distance(_foodCouple[0].transform.position, _foodCouple[1].transform.position) > 1.51f)
            {
                Debug.Log(Vector3.Distance(_foodCouple[0].transform.position, _foodCouple[1].transform.position));
                return false;
            }
                

            List<(Food, (int, int))> horizontalCheck = new List<(Food, (int, int))>();
            List<(Food, (int, int))> verticalCheck = new List<(Food, (int, int))>();

            List<Food> result = new List<Food>();
            
            foreach (var food in _foodCouple)
            {
                for (int ind = -2; ind < 3; ind++)
                {
                    try
                    {
                        (int, int) indexHorizontal = (food.index2DArray.Item1 + ind, food.index2DArray.Item2);
                        if (_foodSpawner.FoodGameArea[indexHorizontal.Item1, indexHorizontal.Item2] != null)
                        {
                            horizontalCheck.Add((_foodSpawner.FoodGameArea[indexHorizontal.Item1, indexHorizontal.Item2], indexHorizontal));
                            //Debug.Log((_foodSpawner.FoodGameArea[indexHorizontal.Item1, indexHorizontal.Item2], indexHorizontal));
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        continue;
                    }
                } 
                
                for (int ind = -2; ind < 3; ind++)
                {
                    try
                    {
                        (int, int) indexVertical = (food.index2DArray.Item1, food.index2DArray.Item2 + ind);
                        if (_foodSpawner.FoodGameArea[indexVertical.Item1, indexVertical.Item2] != null)
                        {
                            verticalCheck.Add((_foodSpawner.FoodGameArea[indexVertical.Item1, indexVertical.Item2], indexVertical));
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        continue;
                    }
                }
                var correctsHorizontal = CheckTrueCoordinates(horizontalCheck);
                foreach (var value in correctsHorizontal)
                {
                    try
                    {
                        Destroy(value.Item1.gameObject);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            
                var correctsVertical = CheckTrueCoordinates(verticalCheck);
                foreach (var value in correctsVertical)
                {
                    try
                    {
                        Destroy(value.Item1.gameObject);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                
                horizontalCheck.Clear();
                verticalCheck.Clear();
            }
            
            
            return true;
        }

        private List<(Food, (int, int))> CheckTrueCoordinates(List<(Food, (int, int))> list )
        {
            //Debug.Log($"List count:{list.Count}");
            List<(Food, (int, int))> result = new List<(Food, (int, int))>();
            
            for (int ind = 0; ind < list.Count; ind++)
            {
                //Debug.Log($"List ind: {list[ind]}");
                if (ind != 0)
                {
                    if (result.Count != 0)
                    {
                        if (list[ind].Item1.foodType != result[0].Item1.foodType)
                        {
                            if (result.Count < 3)
                            {
                                result.Clear();
                                result.Add(list[ind]);
                            }
                            else
                            {
                                return result;
                            }
                        }
                        else
                        {
                            result.Add(list[ind]);
                        }
                    }
                    else
                    {
                        result.Add(list[ind]);
                    }
                    
                }
                else
                {
                    result.Add(list[ind]);
                }
            }

            if (result.Count < 3)
            {
                result.Clear();
            }
            
            return result;
        }
        
    }
}
