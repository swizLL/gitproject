using UnityEngine;
using System.Collections;

public class turnRightt : MonoBehaviour {
    public dogemove doge;

    private Transform player;
    private bool isPress;
	// Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void OnPress(bool isPress)
    {
        this.isPress = isPress;
    }

        // Update is called once per frame
        void Update () {

        if (isPress)
        {
            player.Rotate(Vector3.up * Time.deltaTime * doge.rotspeed);
        }
	}
}
