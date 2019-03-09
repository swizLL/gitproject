using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerVillageAnimation : MonoBehaviour {
    private Animator anim;
    private new Rigidbody rigidbody = new Rigidbody();
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		anim=this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody.velocity.magnitude>0.5f||agent.enabled)
        {
            anim.SetBool("Move", true);
        }
        else
        {
            anim.SetBool("Move", false);
        }
	}
} 
