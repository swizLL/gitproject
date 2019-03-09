using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class followCamera : MonoBehaviour
{
    
    private Transform target;
    private Vector3 pos;

    public float speed = 5;
    public Vector3 offset = new Vector3(0, -1.1f, -20f);//相机相对于玩家的位置
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

    // Update is called once per frame
    void Update()
    {
        pos = target.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, speed * Time.deltaTime);//调整相机与玩家之间的距离
    }
}
