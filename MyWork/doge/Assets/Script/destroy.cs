using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {
	public float lifetime = 3;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);	
	}
}
