using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerVillageMove : MonoBehaviour
{

    public float velocity = 10;

    private new Rigidbody rigidbody = new Rigidbody();
    private Quaternion qua = new Quaternion();
    private NavMeshAgent navAgent;
    // Update is called once per frame
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        navAgent = this.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //刚体的速度变量
        Vector3 vec = rigidbody.velocity;
        if (Mathf.Abs(h) >= 0.05f || Mathf.Abs(v) >= 0.05f)
        {
            rigidbody.velocity = new Vector3(-h * velocity, vec.y, -v * velocity);
            //修改人物的朝向，只改变下x，z轴的朝向
            transform.rotation = Quaternion.LookRotation(new Vector3(-h, 0, -v));
        }
        else
        {
            //使人物动作控制看起来更加流畅
            if (navAgent.enabled == false|| Mathf.Abs(h) <= 0.05f || Mathf.Abs(v) <= 0.05f)
            {
                rigidbody.velocity = Vector3.zero;
            }
        }
    }
}
