using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnuGames;
public class WebService : SingletonBehaviour<WebService>
{
    string baseUrl = "http://127.0.0.1:8882/";
    string leaderBoard = "leaderboard";

    public void SendScoreToLeaderBoard(Action<WWW> onComplete, Action<WWW> onError, UserData userData)
    {
        var jsonStr = JsonUtility.ToJson(userData);
        var postCoroutine = this.Post(this.baseUrl + this.leaderBoard, onComplete, onError, jsonStr);
        StartCoroutine(postCoroutine);
    }

    IEnumerator Post(string url, Action<WWW> onComplete, Action<WWW> onError, string jsonStr)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);
        WWW www = new WWW(url, formData, headers);
        yield return www;
        Debug.Log(www.error + " ____ " + www.text);
        if (String.IsNullOrEmpty(www.error))
        {
            if (onComplete != null)
            {
                onComplete(www);
            }

        }
        else
        {
            if (onError != null)
            {
                onError(www);
            }

        }
        yield break;
    }

    public static int getResponseCode(WWW request)
    {
        int ret = 0;
        if (request.responseHeaders == null)
        {
            Debug.LogError("no response headers.");
        }
        else
        {
            if (!request.responseHeaders.ContainsKey("STATUS"))
            {
                Debug.LogError("response headers has no STATUS.");
            }
            else
            {
                ret = parseResponseCode(request.responseHeaders["STATUS"]);
            }
        }

        return ret;
    }

    public static int parseResponseCode(string statusLine)
    {
        int ret = 0;

        string[] components = statusLine.Split(' ');
        if (components.Length < 3)
        {
            Debug.LogError("invalid response status: " + statusLine);
        }
        else
        {
            if (!int.TryParse(components[1], out ret))
            {
                Debug.LogError("invalid response code: " + components[1]);
            }
        }

        return ret;
    }
}