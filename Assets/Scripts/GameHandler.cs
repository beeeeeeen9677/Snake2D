using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    //scene size
    [SerializeField]
    private int width; 
    [SerializeField]
    private int height;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private List<GameObject> foodPrefabs;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SpawnFood();
    }

    public void SpawnFood()
    {
        int foodIdx = Random.Range(0, foodPrefabs.Count);
        
        Vector3Int randomPosition;
        do{
            randomPosition = new Vector3Int(Random.Range(0, width), Random.Range(0, height), 0);
            //should not spawn on player's position
        } while (randomPosition != player.transform.position);
        

        Instantiate(foodPrefabs[foodIdx], randomPosition, Quaternion.identity);
    }
    
}
