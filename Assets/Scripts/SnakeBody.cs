using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Snake player = collision.gameObject.GetComponent<Snake>();
            if (player != null)
            {
                if (player.GetIndexOfBody(gameObject) == 1)//first body always touching head
                    return;
                //body is touched by head
                GameHandler.instance.EndGame("Body was touched by head");
            }
        }
    }
}
