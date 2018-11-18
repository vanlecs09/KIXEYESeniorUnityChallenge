
using System;
[Serializable]
public class UserData
{
    public UserData()
    {
        UserName = "some-user-name";
        Score = 0;
    }
    public string UserName;
    public int Score;
}

public class GameData
{
    private static GameData instance;
    GameData()
    {
        UserData = new UserData();
    }
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }

            return instance;
        }
    }
    public UserData UserData {get; set;}
}