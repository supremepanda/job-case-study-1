using System;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level1
{
    public class Progress : MonoBehaviour, IProgress
    {
        [Tooltip("Total unit amount to calculate percentage per unit")]
        [SerializeField] private int totalUnit;
        
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TMP_Text progressText;
        
        private float _addPercentagePerOlive;
        private float _percentage;
        
        private FinalPanel _finalPanel;
        
        private void Start()
        {
            _addPercentagePerOlive = 100f / totalUnit;
            _finalPanel = FindObjectOfType<FinalPanel>();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < 64; i++)
                {
                    IncreaseProgress();
                }
            }
#endif
        }

        public void IncreaseProgress()
        {
            _percentage += _addPercentagePerOlive;
            progressSlider.value = _percentage;
            progressText.text = $"%{_percentage}";

            if (_percentage >= 100f)
            {
                _finalPanel.ActivateFinalPanel();
            }
        }
    }
}