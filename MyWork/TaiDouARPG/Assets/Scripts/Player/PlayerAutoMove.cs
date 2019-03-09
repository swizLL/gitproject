using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerAutoMove : MonoBehaviour {

    private NavMeshAgent agent;
    private Animator anim;
	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.enabled)
        {
            //距离小于距目标点的最小距离,或者按下方向键就停止导航
            if (agent.remainingDistance < agent.stoppingDistance&&agent.remainingDistance!=0)
            {
                agent.isStopped=true;
                agent.enabled = false;
                TaskManager._instance.onArrivePos();
            }
            //如果导航中按下方向键，导航停止
            if (Mathf.Abs(Input.GetAxis("Horizontal"))>0.05f||Mathf.Abs(Input.GetAxis("Vertical"))>0.05f)
            {
                agent.isStopped = true;
                agent.enabled = false;
            }
        }
	}
    public void setTargetPosition(Vector3 targetPos)
    {
        agent.enabled = true;
        agent.SetDestination(targetPos);
    }
}
