using System;
using UnityEngine;
using UnuGames;
public enum LeaderBoardReponseCode
{
    USER_NOT_FOUND = 404,
    OK = 200,
    INVALID_USER_NAME = 405,
}
public class LeaderBoard
{
    private static LeaderBoard instance;
    public static LeaderBoard Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LeaderBoard();
            }

            return instance;
        }
    }

    public void SendScoreToLeaderBoard()
    {
        // UIMan.Instance.show
        WebService.Instance.SendScoreToLeaderBoard(OnComplete, OnError, GameData.Instance.UserData);
    }

    void OnComplete(WWW www)
    {
        ShowMessage(WebService.getResponseCode(www));
    }

    void OnError(WWW www)
    {
        ShowMessage(WebService.getResponseCode(www), www.error);
    }

    void ShowMessage(int responseCode, string errorText = "unknow error")
    {
        switch (responseCode)
        {
            case (int)LeaderBoardReponseCode.OK:
                {
                    UIMan.Instance.ShowPopup("leader board ", "Success update data to leaderboard");
                    break;
                }
            case (int)LeaderBoardReponseCode.USER_NOT_FOUND:

                {
                    UIMan.Instance.ShowPopup("leader board ", "Error : User not found");
                    break;
                }
            case (int)LeaderBoardReponseCode.INVALID_USER_NAME:
                {
                    UIMan.Instance.ShowPopup("leader board ", "Error : Invalid user name");
                    break;
                }
            default:
                {
                    UIMan.Instance.ShowPopup("leader board ", errorText);
                    break;
                }
        }
    }
}