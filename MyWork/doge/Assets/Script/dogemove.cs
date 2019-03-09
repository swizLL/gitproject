using UnityEngine;
using System.Collections;

public class dogemove : MonoBehaviour {

	public float speed = 4;
    public float rotspeed = 200;
    public float hp = 100;
    public GameObject enemy;

    private float angel;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
        move();
        runSpace();
        if (this.hp >= 100)
        {
            this.hp = 100;
        }
    }
    void runSpace()
    {
        if (transform.position.x <= -28)
        {
            transform.position = new Vector3(-27.9f, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= 22.5f)
        {
            transform.position = new Vector3(22.4f, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= 41)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 40.9f);
        }
        if (transform.position.z <= -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -9.9f);
        }

    }
    void move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -rotspeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotspeed);
        }
    }
}



