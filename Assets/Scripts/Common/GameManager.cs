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
            Debug.Log($"width: {Screen.width}, height: {Screen.height}");
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                InitializeManager();
            }
        }
    }
}