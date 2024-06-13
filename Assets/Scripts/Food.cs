using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    private string text;
    [SerializeField]
    private int score;
    [SerializeField]
    private AudioClip[] eatSound;
    private AudioSource audioSource;
    private Sprite bodySprite;
    [SerializeField]
    private Text ui_text;
    private string category;
    [SerializeField]
    private DBTSO dbtData;
    [SerializeField]
    private Sprite[] bubbleSprites;
    [SerializeField]
    private Sprite[] bodySprites;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (dbtData != null)
            InitFood();
    }




    private void InitFood()
    {
        text = dbtData.text.Trim();
        //GetComponent<SpriteRenderer>().sprite = dbtData.bubbleSprite;
        //bodySprite = dbtData.bodySprite;
        category = dbtData.category.Trim();
        ChangeBubbleSprite();




        //calculate the size of text
        //min: 35    max: 60
        //normalize    (n-2)/(5-2)   at most 5 char for 1 line
        int textLength = text.Length;
        textLength = Mathf.Clamp(textLength, 2, 5);

        ui_text.fontSize = (int)(60 - 25 * ((textLength - 2) / 3));
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



        if (category == "有问题")
            StartCoroutine(CountDownDisappear());

    }



    IEnumerator CountDownDisappear()
    {
        yield return new WaitForSeconds(24);
        Animator anim = GetComponent<Animator>();
        anim.enabled = true; //flashing effect
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
        GameHandler.instance.SpawnFood();
    }

    private void ChangeBubbleSprite()
    {
        int categoryIdx;

        switch (category)
        {
            case "正念":
                categoryIdx = 0;
                break;
            case "辨别并了解情绪":
                categoryIdx = 1;
                break;
            case "核对事实":
                categoryIdx = 2;
                break;
            case "辩证思维":
                categoryIdx = 3;
                break;
            case "舒缓":
                categoryIdx = 4;
                break;
            case "DEARMAN":
                categoryIdx = 5;
                break;
            case "全然接纳":
                categoryIdx = 6;
                break;
            case "解决问题":
                categoryIdx = 7;
                break;
            case "立即停止":
                categoryIdx = 8;
                break;
            case "自我抚慰":
                categoryIdx = 9;
                break;
            case "智慧心":
                categoryIdx = 10;
                break;
            case "建立自我掌控":
                categoryIdx = 11;
                break;





            case "有问题":
                categoryIdx = -1;
                break;
            default:
                categoryIdx = -1;
                //Debug.Log("no such category");
                break;
        }

        if (categoryIdx == -1)
            RandomBubbleSprite();
        else
        {
            bodySprite = bodySprites[categoryIdx];
            GetComponent<SpriteRenderer>().sprite = bubbleSprites[categoryIdx];
        }
    }

    private void RandomBubbleSprite()
    {
        GetComponent<SpriteRenderer>().sprite = bubbleSprites[UnityEngine.Random.Range(0, bubbleSprites.Length)];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("eat");
        if (collision.tag == "Player")
        {
            Snake player = collision.GetComponent<Snake>();
            if (player == null)
                return;


            GetComponent<BoxCollider2D>().enabled = false;


            if (category == "有问题")
                audioSource.PlayOneShot(eatSound[1]);
            else
            {
                player.AddBodySize(bodySprite);
                audioSource.PlayOneShot(eatSound[0]);
            }


            GameHandler.instance.EatFood(text, score, category);
            StartCoroutine(Shrink());


            //GameHandler.instance.SpawnFood();
            //GameHandler.instance.AddHP(score / 5);
            //GameHandler.instance.EatFood(text, score, category);
        }
    }


    IEnumerator Shrink()
    {
        //GetComponent<BoxCollider2D>().enabled = false;
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
        if (gameObject != null)
            Destroy(gameObject);
    }


    public void SetDBTData(DBTSO data)
    {
        dbtData = data;
        InitFood();
    }

}
