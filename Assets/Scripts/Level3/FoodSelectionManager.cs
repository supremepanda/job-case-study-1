using System;
using Common;
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
        private ApplicationType _applicationType;

        private void ClearSelections(Food food1, Food food2)
        {   
            Debug.Log("Launch");
            _selectionResponse.OnDeselect(food1.transform);
            _selectionResponse.OnDeselect(food2.transform);
        }
        
        private void Awake()
        {
            _selectionResponse = GetComponent<ISelectionResponse>();
        }

        private void Start()
        {
            _applicationType = FindObjectOfType<GameManager>().ApplicationType;
            _gameControl = FindObjectOfType<CandyCrushGameControl>();
            _gameControl.TwoFoodCollected += ClearSelections;
        }

        private void Update()
        {
            if (_applicationType == ApplicationType.Editor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray(Input.mousePosition);
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        Ray(touch.position);
                    }
                }
            }
        }
        private void Ray(Vector3 position)
        {
            var ray = Camera.main.ScreenPointToRay(position);
            _selection = null;
            if (Physics.Raycast(ray, out var hit))
            {
                var selection = hit.transform;
                Debug.Log(hit.transform);
                if (selection.CompareTag(selectableTag))
                {
                    _selection = selection;
                }
            }
            Selection();
        }
        
        private void Selection()
        {
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
        }
    }
}
