using UnityEngine;
using System.Collections;

public class donghua : MonoBehaviour {
	public AnimationCurve a;
	Vector3 s;
	public float playspeed=3;
	float timeoffset=0;
	// Use this for initialization
	void Start () {
		s = transform.localScale;
		timeoffset = Random.value;
	}
	
	// Update is called once per frame
	void Update () {
		timeoffset += Time.deltaTime;
		float r = a.Evaluate (timeoffset * playspeed);
		transform.localScale = new Vector3 (s.x, s.y * r, s.z);
	}
}
