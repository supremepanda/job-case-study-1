using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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