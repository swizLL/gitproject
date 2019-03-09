using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour {

    public Transform[] waypoints;

    void OnDrawGizmos()
    {
        iTween.DrawPath(waypoints, Color.yellow);
    }
}
