using UnityEngine;
using System.Collections;

public class bone : MonoBehaviour {
    public GameObject player;
    public float rehealth=5;
	// Use this for initialization
	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Enemy") {
			coll.GetComponent <jiangshimove> ().Hurt ();
            player.GetComponent<dogemove>().hp += rehealth;            
		}
	}
}
