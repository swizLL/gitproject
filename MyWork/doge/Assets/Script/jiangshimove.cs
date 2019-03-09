using UnityEngine;
using System.Collections;

public class jiangshimove : MonoBehaviour {
	public Transform player;
	public float rotSpeed;
	public Vector3 vc;
	public GameObject jiangshi;
	public GameObject blood;
	// Use this for initialization
	void Start () {
		player = Manager.m.player;	
	}
	
	// Update is called once per frame
	void Update () {
        move();
        moveSpace();
    }
	public void Hurt(){
		Destroy (jiangshi);
		Instantiate (blood, new Vector3(transform.position.x,transform.position.y+2,transform.position.z), Quaternion.identity);
        Manager.m.point += 5;
	}
    void moveSpace()
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
        Vector3 targetPos= getNewTarget();
        Vector3 targetDir =targetPos  - transform.position;
        float step = rotSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.Translate(Vector3.forward * Time.deltaTime * -11);
        if (transform.position==targetPos) {
            targetPos = getNewTarget();
        }
    }
    Vector3 getNewTarget()
    {
        Vector3 targetpos = new Vector3(Random.Range(-28, 22.5f), 0, Random.Range(-10, 41));
        return targetpos;
    }
}
