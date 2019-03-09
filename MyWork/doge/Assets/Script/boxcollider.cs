using UnityEngine;
using System.Collections;

public class boxcollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<BoxCollider>().enabled = true;
	}
}
