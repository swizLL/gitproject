using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    private Transform targetTranform;
    public Vector3 offset = new Vector3(0,15,25);
    private Quaternion lockRotation = new Quaternion();
    public float smoothing = 1;
    // Use this for initialization
    private void Awake()
    {
        targetTranform = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
        lockRotation = transform.rotation;
    }
    // Update is called once per frame
    void Update () {
        //transform.position = targetTranform.position + offset;
        transform.rotation = lockRotation;
        Vector3 targetPos = targetTranform.position + offset;
        transform.position=Vector3.Lerp(transform.position, targetPos, smoothing*Time.deltaTime);
	}
}
