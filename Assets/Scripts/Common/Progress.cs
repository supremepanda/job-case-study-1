using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Progress : MonoBehaviour, IProgress
    {
        [Tooltip("Total unit amount to calculate percentage per unit")]
        [SerializeField] private int totalUnit;
        
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TMP_Text progressText;
        
        private float _addPercentagePerItem;
        private float _percentage;
        
        private FinalPanel _finalPanel;
        
        private void Start()
        {
            _finalPanel = FindObjectOfType<FinalPanel>();
            _addPercentagePerItem = 100f / totalUnit;
        }
        
        public void IncreaseProgress()
        {
            _percentage += _addPercentagePerItem;
            progressSlider.value = _percentage;
            progressText.text = $"%{_percentage}";

            if (_percentage >= 100f)
            {
                _percentage = 100;
                progressSlider.value = _percentage;
                progressText.text = $"%{_percentage}";
                _finalPanel.ActivateFinalPanel();
            }
        }
    }
}