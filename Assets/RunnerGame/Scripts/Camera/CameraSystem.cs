using UnityEngine;

public class CameraSystem
{
    Transform _playerTrans;
    Transform _cameraTrans;
    public void Init(Transform playerTrans)
    {
        _playerTrans = playerTrans;
        _cameraTrans = Camera.main.transform;
    }

    public void Update(float deta)
    {

    }

    public void FixedUpdate(float delta)
    {
       
    }

    public void LateUpdate(float delta)
    {
        var playerPos = _playerTrans.position;
        var cameraPos = _cameraTrans.position;
        _cameraTrans.position = new Vector3(playerPos.x, cameraPos.y, cameraPos.z);
    }

    public void Shutdown()
    {

    }
}