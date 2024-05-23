using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float distanceBetween = 0.2f;
    private float countUp = 0;
    [SerializeField]
    private float rotateSpeed;
    private float inputDirection;//player inputing direction
    private Rigidbody2D rb;
    private int bodyLength;
    [SerializeField]
    private GameObject bodyPrefab;
    private List<GameObject> snakeBody = new List<GameObject>();
    [SerializeField]
    private List<Sprite> bodySpriteStack = new List<Sprite>();

    [SerializeField]
    private MoveButton leftBtn;
    [SerializeField]
    private MoveButton rightBtn;





    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //inputDirection = Vector2Int.down;
        inputDirection = 0;
        bodyLength = 3;

        snakeBody.Add(this.gameObject);//snake head
        countUp = 0;



        //InvokeRepeating("ChangeGridPosition", 0, gridMoveTimerMax);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetPlayerInput();
        //Debug.Log(gridPosition);
        Move();
        //Debug.Log(inputDirection);
        if (snakeBody.Count < bodyLength)
            CreateNewBody();

    }

    private void Move()
    {
        //head
        if (inputDirection != 0)
            transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.fixedDeltaTime * inputDirection));
        rb.velocity = transform.right * speed * Time.fixedDeltaTime;

        //body
        if (snakeBody.Count > 1)//have body
        {
            for (int i = 1; i < snakeBody.Count; i++)//start from first body
            {
                //get the position and rotation from previous body
                MarkerManager mm = snakeBody[i - 1].GetComponent<MarkerManager>();
                snakeBody[i].transform.position = mm.markerList[0].posistion;
                snakeBody[i].transform.rotation = mm.markerList[0].rotation;
                mm.markerList.RemoveAt(0);//remove the oldest position
            }
        }
    }


    /*
    private float GetAngleFromVector(Vector2 gridMoveDirection)
    {
        float angle = Mathf.Atan2(gridMoveDirection.y, gridMoveDirection.x)*Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }
        return angle;
    }
    */

    private void GetPlayerInput()
    {
        //handle keyboard
        inputDirection = Input.GetAxis("Horizontal");

        //handle button on screen
        if (leftBtn.isPressing)
            inputDirection = -1;
        if (rightBtn.isPressing)
            inputDirection = +1;

        if (leftBtn.isPressing && rightBtn.isPressing)
            inputDirection = 0;
    }


    public void AddBodySize(Sprite img)
    //img: the body sprite after eating food
    {
        bodySpriteStack.Add(img);
        bodyLength++;
        //bodyList.Add(newBody.transform);
    }

    private void CreateNewBody()
    {
        MarkerManager mm = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp == 0)
        {
            mm.ClearMarkerList();
        }
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            GameObject newBody = Instantiate(bodyPrefab, mm.markerList[0].posistion, mm.markerList[0].rotation, transform.parent);
            newBody.GetComponent<SpriteRenderer>().sprite = bodySpriteStack[0];
            bodySpriteStack.RemoveAt(0);
            snakeBody.Add(newBody);
            newBody.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
        }
    }

    public int GetIndexOfBody(GameObject body)
    {
        return snakeBody.IndexOf(body);
    }


}
