using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;
    // Start is called before the first frame update
    void Awake()
    {
        var random = new System.Random((int)transform.position.x);
        // Generate a random number between 0 and dbtListLength
        int randomIdx = random.Next(0, sprites.Count);
        GetComponent<SpriteRenderer>().sprite = sprites[randomIdx];
    }
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
                        msg = "你撞到障礙物了";
                        break;
                    case 1:
                        msg = "你撞到障碍物了";
                        break;
                    default:
                        msg = "You have touched an obstacle";
                        break;
                }
                //Debug.Log("Lose");
                GameHandler.instance.EndGame(msg);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            //Debug.Log("stone collides with food");
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
                GameHandler.instance.SpawnFood();
            }
        }
    }


}
