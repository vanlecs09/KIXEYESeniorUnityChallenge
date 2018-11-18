using UnityEngine;
using UnuGames;

public class PlayerCollisionSystem : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == GameTags.OBSTACLE)
        {
            UGame.EventManager.TriggerEvent(EventNames.COLLISION_WTIH_OBSTACLE);
        }
        else if (col.gameObject.tag == GameTags.FLOOR)
        {
            UGame.EventManager.TriggerEvent(EventNames.COLLISION_WITH_FLOOR);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == GameTags.OBSTACLE)
        {
            UGame.EventManager.TriggerEvent(EventNames.GMAE_OVER);
        }
        else if (col.gameObject.tag == GameTags.FLOOR)
        {
            UGame.EventManager.TriggerEvent(EventNames.EXIT_COLLISION_WITH_FLOOR);
        }
    }
}
