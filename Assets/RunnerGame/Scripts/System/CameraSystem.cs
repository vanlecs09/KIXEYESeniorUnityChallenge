using UnityEngine;

public class CameraSystem
{
    Transform playerTrans;
    Transform cameraTrans;
    public void Init(Transform _playerTrans)
    {
        playerTrans = _playerTrans;
        cameraTrans = Camera.main.transform;
    }

    public void Update(float deta)
    {

    }

    public void FixedUpdate(float delta)
    {
       
    }

    public void LateUpdate(float delta)
    {
        var playerPos = playerTrans.position;
        var cameraPos = cameraTrans.position;
        cameraTrans.position = new Vector3(playerPos.x, cameraPos.y, cameraPos.z);
    }

    public void Shutdown()
    {

    }
}