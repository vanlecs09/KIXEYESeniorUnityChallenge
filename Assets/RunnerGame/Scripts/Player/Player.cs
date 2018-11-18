using UnityEngine;

public class Player
{
    GameObject _playerGameObject;
    Transform _playerTrans;
    Rigidbody2D _playerRigibody;

    PlayerConfig _playerConfig;

    bool _isOnGround;

    public Transform PlayerTrans
    {
        get
        {
            return _playerTrans;
        }
    }
    public void Init()
    {
        var playerPrefab = ResourceFactory.Load<GameObject>("Player/pref_Player");
        _playerGameObject = GameObject.Instantiate(playerPrefab);
        _playerTrans = _playerGameObject.transform;

        _playerConfig = _playerGameObject.GetComponent<PlayerConfig>();

        _playerRigibody = _playerGameObject.GetComponent<Rigidbody2D>();
        _playerRigibody.AddForce(Vector2.right * 7, ForceMode2D.Impulse);
        _isOnGround = false;

        UGame.EventManager.StartListening(EventNames.EXIT_COLLISION_WITH_FLOOR, NotOnGround);
        UGame.EventManager.StartListening(EventNames.COLLISION_WITH_FLOOR, OnGround);
    }

    public void Update(float delta)
    {
    }

    public void FixedUpdate(float delta)
    {

    }

    public void Shutdown()
    {
        GameObject.Destroy(_playerGameObject);
        UGame.EventManager.StopListening(EventNames.EXIT_COLLISION_WITH_FLOOR, NotOnGround);
        UGame.EventManager.StopListening(EventNames.COLLISION_WITH_FLOOR, OnGround);
    }

    public void Jump()
    {
        if (_isOnGround)
        {
            _playerRigibody.AddForce(Vector2.up * _playerConfig.JumpSpeed, ForceMode2D.Force);
        }
    }

    public void OnGround(System.Object obj = null)
    {
        _isOnGround = true;
    }

    public void NotOnGround(System.Object obj = null)
    {
        _isOnGround = false;
    }
}