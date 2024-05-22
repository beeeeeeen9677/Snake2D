using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private AudioClip eatSound;
    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("eat");
        if(collision.tag == "Player")
        {
            //AudioSource.PlayClipAtPoint(eatSound, transform.position);
            audioSource.PlayOneShot(eatSound);
            //Destroy(gameObject);
            StartCoroutine(Shrink());
            GetComponent<BoxCollider2D>().enabled = false;
            GameHandler.instance.SpawnFood();
        }
    }

    IEnumerator Shrink()
    {
        Vector2 startSize = transform.localScale;
        Vector2 endSize = Vector2.zero;
        float timer = 0.5f;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(endSize, startSize, timer/0.5f);
            yield return null;
        }
        Destroy(gameObject);
    }

}
