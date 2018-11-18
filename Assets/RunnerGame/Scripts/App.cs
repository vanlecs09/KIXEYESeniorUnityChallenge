using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnuGames;

public class App : MonoBehaviour
{
    void Start()
    {
        UIMan.Instance.ShowScreen<UIStartScreen>();
    }
}
