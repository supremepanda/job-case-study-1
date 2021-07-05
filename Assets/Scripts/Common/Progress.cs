using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Common
{
    public class Progress : MonoBehaviour, IProgress
    {
        public delegate void GameEnd();

        public event GameEnd OnGameEnd;
        
        [Tooltip("Total unit amount to calculate percentage per unit")]
        [SerializeField] private int totalUnit;
        
        [SerializeField] private Slider progressSlider;
        [SerializeField] private TMP_Text progressText;
        
        private float _addPercentagePerItem;
        private float _percentage;
        
        private FinalPanel _finalPanel;
        private GameManager _gameManager;
        
        private void Start()
        {
            _finalPanel = FindObjectOfType<FinalPanel>();
            _gameManager = FindObjectOfType<GameManager>();
            
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
                GiveReward();
                OnGameEnd?.Invoke();
            }
        }

        private void GiveReward()
        {
            // Build index determines reward of level. (Level 1 build index = 1, so reward index = 0)
            int rewardIndex = SceneManager.GetActiveScene().buildIndex - 1;
            _gameManager.AddReward(rewardIndex);
        }
    }
}