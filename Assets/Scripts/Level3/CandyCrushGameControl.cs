using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Level3
{
    public class CandyCrushGameControl : MonoBehaviour
    {
        // Trigger event when two food selected.
        public delegate void SelectTwoFood(Food food1, Food food2);
        
        public event SelectTwoFood TwoFoodSelected;
        
        private FoodSelectionManager _foodSelectionManager;
        
        // List to store selected foods.
        private List<Food> _foodCouple = new List<Food>();
        
        private (Vector3, Vector3) _firstPositions;

        private FoodSpawner _foodSpawner;
        private Progress _progress;
        
        private void Start()
        {
            _foodSelectionManager = FindObjectOfType<FoodSelectionManager>();
            _foodSpawner = FindObjectOfType<FoodSpawner>();
            _progress = FindObjectOfType<Progress>();
            _foodSelectionManager.OnSelectedFood += AddFood;
        }

        /// <summary>
        /// Add food to control game rules. When two object selected, this function calls CheckRules function.
        /// </summary>
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
                        TwoFoodSelected?.Invoke(_foodCouple[0], _foodCouple[1]);
                        ClearFoods();
                    }
                    else
                    {
                        TwoFoodSelected?.Invoke(_foodCouple[0], _foodCouple[1]);
                        StartCoroutine(ReversePositions());
                    }
                }
            }
        }

        private void ClearFoods()
        {
            _foodCouple.RemoveRange(0, 2);
            
        }
        
        /// <summary>
        /// Change food positions without any conditions. It also saves first positions of foods to reverse this action.
        /// </summary>
        private void ChangeFoodPositions()
        {
            var food1 = _foodCouple[0];
            var food2 = _foodCouple[1];

            var position1 = food1.transform.position;
            var position2 = food2.transform.position;
            
            // Save first positions of foods.
            _firstPositions = (position1, position2);

            // Change food1 position to food2.
            position1 = Vector3.Lerp(position1, _firstPositions.Item2, 1f);
            food1.transform.position = position1;

            // Change food2 position to food1.
            position2 = Vector3.Lerp(position2, _firstPositions.Item1, 1f);
            food2.transform.position = position2;
            
            // Update FoodGameArea which is 2D Food array.
            _foodSpawner.FoodGameArea[food1.index2DArray.Item1, food1.index2DArray.Item2] = food2;
            _foodSpawner.FoodGameArea[food2.index2DArray.Item1, food2.index2DArray.Item2] = food1;
            
            // Update food's index information on FoodGameArea.
            var tempFood1Index = food1.index2DArray;
            food1.index2DArray = food2.index2DArray;
            food2.index2DArray = tempFood1Index;
        }

        /// <summary>
        /// Reverse changed positions.
        /// </summary>
        /// <returns></returns>
        private IEnumerator ReversePositions()
        {
            yield return new WaitForSeconds(1.5f);
            var food1 = _foodCouple[0];
            var food2 = _foodCouple[1];

            // Change food positions.
            food1.transform.position = Vector3.Lerp(food1.transform.position, _firstPositions.Item1, 1f);
            food2.transform.position = Vector3.Lerp(food2.transform.position, _firstPositions.Item2, 1f);
            _firstPositions = (Vector3.zero, Vector3.zero);

            // Update FoodGameArea 2D array.
            _foodSpawner.FoodGameArea[food2.index2DArray.Item1, food2.index2DArray.Item2] = food1;
            _foodSpawner.FoodGameArea[food1.index2DArray.Item1, food1.index2DArray.Item2] = food2;
            
            // Update food's index information on FoodGameArea.
            var food1IndexArray = food1.index2DArray;
            food1.index2DArray = food2.index2DArray;
            food2.index2DArray = food1IndexArray;
            
            ClearFoods();
        }

        /// <summary>
        /// Check nodes related with selected foods to destroy foods or not.
        /// </summary>
        /// <returns></returns>
        private bool CheckRules()
        {
            bool returnResult = false;
            
            if (_foodCouple[0].foodType == _foodCouple[1].foodType) return returnResult;
            if (Vector3.Distance(_foodCouple[0].transform.position, _foodCouple[1].transform.position) > 1.51f)
            {
                return returnResult;
            }
                

            // Horizontal food list +-2 unit from selected food. (<Food, index2D>)
            List<(Food, (int, int))> horizontalCheck = new List<(Food, (int, int))>();
            
            // Vertical food list +-2 unit from selected food. (<Food, index2D>)
            List<(Food, (int, int))> verticalCheck = new List<(Food, (int, int))>();
            
            // Paired food list.
            List<Food> result = new List<Food>();
            
            
            foreach (var food in _foodCouple)
            {
                #region Add related food nodes to their lists

                for (int ind = -2; ind < 3; ind++)
                {
                    try
                    {
                        (int, int) indexHorizontal = (food.index2DArray.Item1 + ind, food.index2DArray.Item2);
                        if (_foodSpawner.FoodGameArea[indexHorizontal.Item1, indexHorizontal.Item2] != null)
                        {
                            horizontalCheck.Add((_foodSpawner.FoodGameArea[indexHorizontal.Item1, indexHorizontal.Item2], indexHorizontal));
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
                #endregion

                #region Check paired foods on horizontal and vertical

                var correctsHorizontal = CheckTrueCoordinates(horizontalCheck);
                foreach (var value in correctsHorizontal)
                {
                    try
                    {
                        StartCoroutine(DestroyObj(value.Item1.gameObject));
                        returnResult = true;
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e);
                    }
                }
            
                var correctsVertical = CheckTrueCoordinates(verticalCheck);
                foreach (var value in correctsVertical)
                {
                    try
                    {
                        StartCoroutine(DestroyObj(value.Item1.gameObject));
                        returnResult = true;

                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e);
                    }
                }

                #endregion
                
                horizontalCheck.Clear();
                verticalCheck.Clear();
            }
            
            
            return returnResult;
        }

        /// <summary>
        /// Check can foods combos and return combo food list.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<(Food, (int, int))> CheckTrueCoordinates(List<(Food, (int, int))> list )
        {
            List<(Food, (int, int))> result = new List<(Food, (int, int))>();
            
            for (int ind = 0; ind < list.Count; ind++)
            {
                if (ind != 0)
                {
                    if (result.Count != 0)
                    {   
                        if (list[ind].Item1.foodType != result[0].Item1.foodType)
                        {   
                            // No combo possible because there are only 1 or 2 same food
                            // and new one's foodType is different.
                            // Clear result and add new one.
                            if (result.Count < 3)
                            {
                                result.Clear();
                                result.Add(list[ind]);
                            }
                            // Result has a combo that's why result should be ended.
                            else
                            {
                                return result;
                            }
                        }
                        // Add new one because there is a combo with same food.
                        else
                        {
                            result.Add(list[ind]);
                        }
                    }
                    // If result has no food, just add new one.
                    else
                    {
                        result.Add(list[ind]);
                    }
                    
                }
                // First action.
                else
                {
                    result.Add(list[ind]);
                }
            }
            
            // If calculated result has less than 3 member, it is not a correct result.
            if (result.Count < 3)
            {
                result.Clear();
            }
            
            return result;
        }
        
        private IEnumerator DestroyObj(GameObject obj)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(obj);
            _progress.IncreaseProgress();
        }
    }
    
    
}
