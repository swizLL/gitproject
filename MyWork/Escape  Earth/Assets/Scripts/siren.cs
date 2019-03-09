using UnityEngine;
using System.Collections;

public class siren : MonoBehaviour {

    public float rotateSpeed=360;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
	}
}
