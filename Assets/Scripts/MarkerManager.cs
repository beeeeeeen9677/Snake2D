using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
    public class Marker
    {
        public Vector3 posistion;
        public Quaternion rotation;

        public Marker(Vector3 posistion, Quaternion rotation)
        {
            this.posistion = posistion;
            this.rotation = rotation;
        }
    }

    public List<Marker> markerList = new List<Marker>();

    private void FixedUpdate()
    {
        UpdateMarkerList();
    }

    private void UpdateMarkerList()
    {
        //track the passed position
        markerList.Add(new Marker(transform.position, transform.rotation));
    }
    public void ClearMarkerList()
    {
        markerList.Clear();
        markerList.Add(new Marker(transform.position, transform.rotation));
    }
}
