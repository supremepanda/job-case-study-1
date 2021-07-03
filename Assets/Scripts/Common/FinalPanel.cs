using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class FinalPanel : MonoBehaviour
    {
        private GameObject _finalPanel;
        private TMP_Text _congratsText;

        public void ActivateFinalPanel()
        {
            _congratsText.text = $"{SceneManager.GetActiveScene().name} Completed!";
            _finalPanel.SetActive(true);
        }
    
        private void Start()
        {
            _finalPanel = transform.Find("FinalPanel").gameObject;
            _congratsText = _finalPanel.transform.Find("CongratsText").GetComponent<TMP_Text>();
        }
    }
}
