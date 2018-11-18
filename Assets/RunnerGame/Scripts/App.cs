using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnuGames;

public class App : MonoBehaviour
{
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        #if UNITY_EDITOR
        Application.targetFrameRate = 60;
        #elif UNITY_ANDROID || UNITY_IOS
        Application.targetFrameRate = 60;
        #endif
    }
    void Start()
    {
        UIMan.Instance.ShowScreen<UIStartScreen>();
    }
}
