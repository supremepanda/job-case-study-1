using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public enum ApplicationType
    {
        Editor,
        Build
    }

    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        private ApplicationType _applicationType;
        public ApplicationType ApplicationType => _applicationType;

        public PlayerRewards playerRewards;
        
        public void AddReward(int index)
        {
            playerRewards.rewardArray[index] = 1;
            PlayerPrefs.SetString("REWARDS", JsonUtility.ToJson(playerRewards, false));
        }

        private void InitializeRewards()
        {
            if (PlayerPrefs.HasKey("REWARDS"))
            {
                playerRewards = JsonUtility.FromJson<PlayerRewards>(PlayerPrefs.GetString("REWARDS"));
            }
            else
            {
                string rewardsJson = "{\"rewardArray\": [0, 0, 0, 0]}";
                PlayerPrefs.SetString("REWARDS", rewardsJson);
                playerRewards = JsonUtility.FromJson<PlayerRewards>(rewardsJson);
            }
        }

        private void InitializeManager()
        {
#if UNITY_EDITOR
            _instance._applicationType = ApplicationType.Editor;
#elif UNITY_ANDROID
            _instance._applicationType = ApplicationType.Build;
#endif
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                InitializeManager();
                InitializeRewards();
            }
        }
    }
}