using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    [SerializeField]
    private string text;
    [SerializeField]
    private int score;
    [SerializeField]
    private AudioClip eatSound;
    private AudioSource audioSource;
    [SerializeField]
    private Sprite bodySprite;
    [SerializeField]
    private Text ui_text;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        //calculate the size of text
        //min: 35    max: 70
        //normalize    (n-2)/(5-2)   at most 5 char for 1 line
        int textLength = text.Length;
        textLength = Mathf.Clamp(textLength, 2, 5);

        ui_text.fontSize = (int)(70 - 35 * ((textLength - 2) / 3));
        //change line
        StringBuilder builder = new StringBuilder();
        int count = 0;
        foreach (char c in text)
        {
            builder.Append(c);
            if ((++count % 5) == 0)
            {
                builder.Append('\n');
            }
        }
        ui_text.text = builder.ToString();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("eat");
        if (collision.tag == "Player")
        {
            Snake player = collision.GetComponent<Snake>();
            if (player == null)
                return;

            player.AddBodySize(bodySprite);

            //AudioSource.PlayClipAtPoint(eatSound, transform.position);
            audioSource.PlayOneShot(eatSound);
            StartCoroutine(Shrink());
            GetComponent<CircleCollider2D>().enabled = false;
            //GameHandler.instance.SpawnFood();
            //GameHandler.instance.AddHP(score / 5);
            GameHandler.instance.EatFood(text, score);
        }
    }


    IEnumerator Shrink()
    {
        GetComponentInChildren<Canvas>().enabled = false;//hide the food name

        Vector2 startSize = transform.localScale;
        Vector2 endSize = Vector2.zero;
        float timer = 0.5f;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(endSize, startSize, timer / 0.5f);
            yield return null;
        }
        Destroy(gameObject);
    }

}
