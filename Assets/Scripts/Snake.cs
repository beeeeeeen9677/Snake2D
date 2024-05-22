using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;
    //private float gridMoveTimer;
    private float gridMoveTimerMax;//move interval
    private Vector2Int inputGridDirection;//player inputing direction
    private Vector2Int gridMoveDirection;//actual current moving direction

    // Start is called before the first frame update
    private void Awake()
    {
        gridPosition = Vector2Int.zero;
        gridMoveTimerMax = 0.25f;
        //gridMoveTimer = gridMoveTimerMax;
        inputGridDirection = Vector2Int.down;
        gridMoveDirection = Vector2Int.down;


        InvokeRepeating("ChangeGridPosition", 0, gridMoveTimerMax);
    }

    // Update is called once per frame
    private void Update()
    {
        GetPlayerInput();
        //Debug.Log(gridPosition);

        
    }

    private void ChangeGridPosition()
    {
        gridMoveDirection = inputGridDirection;
        gridPosition += gridMoveDirection;
        //change position
        transform.position = (Vector2)gridPosition;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) + 90);
    }

    private float GetAngleFromVector(Vector2Int gridMoveDirection)
    {
        float angle = Mathf.Atan2(gridMoveDirection.y, gridMoveDirection.x)*Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    private void GetPlayerInput()
    {
        //get player input
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDirection == Vector2Int.down)
                return;
            inputGridDirection = Vector2Int.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection == Vector2Int.up)
                return;
            inputGridDirection = Vector2Int.down;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection == Vector2Int.right)
                return;
            inputGridDirection = Vector2Int.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection == Vector2Int.left)
                return;
            inputGridDirection = Vector2Int.right;
        }
    }

}
