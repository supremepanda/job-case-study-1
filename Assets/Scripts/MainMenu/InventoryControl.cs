using Common;
using UnityEngine;

namespace MainMenu
{
    public class InventoryControl : MonoBehaviour
    {
        [SerializeField] private Transform[] rewardParentObjects = new Transform[4];

        private PlayerRewards _playerRewards;
        
        private void Start()
        {
            _playerRewards = GameManager.Instance.playerRewards;
            
            for (int ind = 0; ind < _playerRewards.rewardArray.Length; ind++)
            {
                if (_playerRewards.rewardArray[ind] == 1)
                {
                    rewardParentObjects[ind].GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
}
