using UnityEngine;

public class Player
{
    GameObject playerGameObject;
    Transform playerTrans;
    Rigidbody2D playerRigibody;

    PlayerConfig playerConfig;

    public Transform PlayerTrans 
    {
        get 
        { 
            return playerTrans;
        }
    }
    public void Init()
    {
        var playerPrefab = ResourceFactory.Load<GameObject>("Player/player");

        playerGameObject = GameObject.Instantiate(playerPrefab);
        playerTrans = playerGameObject.transform;
        playerRigibody = playerGameObject.GetComponent<Rigidbody2D>();
        playerConfig = playerGameObject.GetComponent<PlayerConfig>();

        playerRigibody.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
    }

    public void Update(float delta)
    {
        
    }

    public void FixedUpdate(float delta)
    {
        
    }

    public void Shutdown(float delta)
    {

    }

    public void Jump()
    {
        playerRigibody.AddForce(Vector2.up * playerConfig.jumpSpeed, ForceMode2D.Force);
    }
}