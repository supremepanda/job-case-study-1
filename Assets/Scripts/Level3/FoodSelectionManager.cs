using System;
using Interfaces;
using UnityEngine;

namespace Level3
{
    public class FoodSelectionManager : MonoBehaviour
    {
        public delegate void SelectFood(Food food);
        public event SelectFood OnSelectedFood;
        
        [SerializeField] private string selectableTag = "Food";
        private ISelectionResponse _selectionResponse;
        private Transform _selection;

        private CandyCrushGameControl _gameControl;

        private void ClearSelections(Food food1, Food food2)
        {   
            _selectionResponse.OnDeselect(food1.transform);
            _selectionResponse.OnDeselect(food2.transform);
        }
        
        private void Awake()
        {
            _selectionResponse = GetComponent<ISelectionResponse>();
        }

        private void Start()
        {
            _gameControl = FindObjectOfType<CandyCrushGameControl>();
            _gameControl.TwoFoodCollected += ClearSelections;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                #region Ray

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                _selection = null;
                if (Physics.Raycast(ray, out var hit))
                {
                    var selection = hit.transform;
                    if(selection.CompareTag(selectableTag))
                    {
                        _selection = selection;
                    }
                }

                #endregion

                if (_selection == null) return;
                if (_selection.GetComponent<Food>().isSelected)
                {
                    _selectionResponse.OnDeselect(_selection);
                }
                else
                {
                    _selectionResponse.OnSelect(_selection);
                    OnSelectedFood?.Invoke(_selection.GetComponent<Food>());
                }
                /*else
                {
                    
                }*/
            }
        }
    }
}
