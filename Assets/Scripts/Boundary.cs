using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Snake player = collision.gameObject.GetComponent<Snake>();
            if (player != null)
            {
                //Debug.Log("Lose");
                GameHandler.instance.EndGame("你撞到墙体了");
            }
        }
    }
}
