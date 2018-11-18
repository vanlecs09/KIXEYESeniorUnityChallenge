using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnuGames;

public class App : MonoBehaviour
{
    void Start()
    {
        #if UNITY_EDITOR
        Application.targetFrameRate = 60;
        #elif UNITY_ANDROID
        Application.targetFrameRate = 60;
        #endif
        UIMan.Instance.ShowScreen<UIStartScreen>();
    }
}
