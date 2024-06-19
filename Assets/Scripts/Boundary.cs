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
                string msg;

                switch (PlayerPrefs.GetInt("Language"))
                {
                    case 0:
                        msg = "你撞到墻體了";
                        break;
                    case 1:
                        msg = "你撞到墙体了";
                        break;
                    default:
                        msg = "You have touched the boundary";
                        break;
                }
                //Debug.Log("Lose");
                GameHandler.instance.EndGame(msg);
            }
        }
    }
}
