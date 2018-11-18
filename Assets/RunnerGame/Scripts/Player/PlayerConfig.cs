using UnityEngine;

public class PlayerConfig : MonoBehaviour
{
    public float JumpSpeed;
    public float MoveSpeed;
    
    void Start()
    {
        JumpSpeed = 300;
        MoveSpeed = 10;
    }
}