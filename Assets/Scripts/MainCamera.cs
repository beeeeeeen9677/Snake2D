using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;//player
    private Vector3 offset;
    void Start()
    {
        offset = Vector3.up * 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
