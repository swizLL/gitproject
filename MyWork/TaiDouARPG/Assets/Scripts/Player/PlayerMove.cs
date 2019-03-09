using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float velocity = 10;
    private new Rigidbody rigidbody;
    private Animator anim;
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 nowVelocity = rigidbody.velocity;
        if (Mathf.Abs(h) > 0.05 || Mathf.Abs(v) > 0.05)
        {
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Empty State"))
            {
                rigidbody.isKinematic = false;
                rigidbody.velocity = new Vector3(velocity * h, nowVelocity.y, v * velocity);
                transform.rotation = Quaternion.LookRotation(new Vector3(h, 0, v));
            }
            else
            {
                rigidbody.isKinematic = true;
                rigidbody.velocity = new Vector3(0, nowVelocity.y, 0);
            }
            anim.SetBool("Move", true);
        }
        else
        {
            //是否遵循动力学，将其设为true，iTween的方法才能实现
            rigidbody.isKinematic = true;
            rigidbody.velocity = new Vector3(0, nowVelocity.y, 0);
            anim.SetBool("Move", false);
        }
    }
}
