using System.Collections;
using System.Linq;
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
    private DBTData dbtData;
    [SerializeField]
    private Sprite[] bubbleSprites;
    [SerializeField]
    private Sprite[] bodySprites;
    [SerializeField]
    public static string[] categoryList;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        transform.Find("Minimap Icon").gameObject.SetActive(true);
    }




    private void InitFood()
    {
        text = dbtData.text.Trim();
        //GetComponent<SpriteRenderer>().sprite = dbtData.bubbleSprite;
        //bodySprite = dbtData.bodySprite;
        category = dbtData.category.Trim();


        //calculate the size of text
        //min: 40    max: 65
        //normalize    (n-2)/(5-2)   at most 5 char for 1 line

        ui_text.text = text;
        ui_text.resizeTextMinSize = PlayerPrefs.GetInt("Language") == 2 ? 1 : 40;//set the min size for best fit
        //Debug.Log(PlayerPrefs.GetInt("Language"));


        //using best fit to resize
        /*
        //resize
        int textLength = text.Length;
        textLength = Mathf.Clamp(textLength, 2, 5);

        ui_text.fontSize = (int)(65 - 25 * ((textLength - 2) / 3));
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
        */



        if (category == categoryList.Last())
            StartCoroutine(CountDownDisappear());

        ChangeBubbleSprite();

    }



    IEnumerator CountDownDisappear()//destroy food after 30 - 40 seconds (for 有問題 food only)
    {
        yield return new WaitForSeconds(30);
        Animator anim = GetComponent<Animator>();
        anim.enabled = true; //flashing effect
        var random = new System.Random(text.Length + category.Length);
        int randomIdx = random.Next(0, 10);
        //Debug.Log(randomIdx);
        yield return new WaitForSeconds(randomIdx);
        Destroy(gameObject);
        GameHandler.instance.SpawnFood();
    }

    private void ChangeBubbleSprite()
    {
        int categoryIdx = -99;


        for (int i = 0; i < categoryList.Length - 1; i++)//skip the last one
        {
            if (category == categoryList[i])
            {
                categoryIdx = i;
                break;
            }
        }
        if (categoryIdx == -99)//有问题 or not exist
        {
            categoryIdx = -1;
            if (category != categoryList.Last())//!=有问题
            {
                //Debug.Log("no such category");
                Debug.Log("food text: " + dbtData.text.Trim());
                Debug.Log("food category: " + dbtData.category.Trim());
            }
        }


        /*
        if (category == "正念")
            categoryIdx = 0;
        else if (category == "辨别并了解情绪")
            categoryIdx = 1;
        else if (category == "核对事实")
            categoryIdx = 2;
        else if (category == "辩证思维")
            categoryIdx = 3;
        else if (category == "舒缓")
            categoryIdx = 4;
        else if (category == "DEARMAN")
            categoryIdx = 5;
        else if (category == "全然接纳")
            categoryIdx = 6;
        else if (category == "解决问题")
            categoryIdx = 7;
        else if (category == "立即停止")
            categoryIdx = 8;
        else if (category == "自我抚慰")
            categoryIdx = 9;
        else if (category == "智慧心")
            categoryIdx = 10;
        else if (category == "建立自我掌控")
            categoryIdx = 11;
        else if (category == "有问题")
            categoryIdx = -1;
        else
        {
            categoryIdx = -1;
            //Debug.Log("no such category");
            Debug.Log("food text: " + text);
            Debug.Log("food category: " + category);
        }
        */




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


            if (category == categoryList.Last())
            {
                audioSource.PlayOneShot(eatSound[1]);
                //Debug.Log("有問題");
            }
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


    public void SetDBTData(DBTData data)
    {
        dbtData = data;
        InitFood();
    }

}
