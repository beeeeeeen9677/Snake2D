using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int score;
    [SerializeField]
    private AudioClip eatSound;
    private AudioSource audioSource;
    [SerializeField]
    private Sprite bodySprite;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
            GetComponent<BoxCollider2D>().enabled = false;
            //GameHandler.instance.SpawnFood();
            //GameHandler.instance.AddHP(score / 5);
            GameHandler.instance.EatFood(name, score);
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
