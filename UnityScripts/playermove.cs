using UnityEngine;
using System.Collections;

public class playermove : MonoBehaviour
{
    //[SerializeField]  //如果你后面是私有变量。加上后可以全部在unity3d中看到。
    public Rigidbody rigidbody;
    private Transform  cameratf;
    public float speed=100f;
    public float rotatespeed=5f;
    public float headspeed;
    public float rotatehead;

    //限制抬头参数
   public float maxrotate, minrotate;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rigidbody = GetComponent<Rigidbody>();
        cameratf = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver= Input.GetAxisRaw("Vertical");
        Vector3 moveDir = hor * transform.right + ver*transform.forward ;
        rigidbody.MovePosition(moveDir.normalized* speed * Time.deltaTime + transform.position);

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        //quaternion 四元数，阻止万象解锁
        rigidbody.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(0, mouseX * rotatespeed, 0) *Time.deltaTime));
        rotatehead += mouseY * headspeed*Time.deltaTime;
        if(rotatehead > maxrotate)
        {
            rotatehead = maxrotate;
        }
        if(rotatehead < minrotate)
        {
            rotatehead = minrotate;
        }
        cameratf.localEulerAngles = new Vector3(-rotatehead, 0, 0);
        if (Input.GetKeyDown(KeyCode.Escape) && !Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}