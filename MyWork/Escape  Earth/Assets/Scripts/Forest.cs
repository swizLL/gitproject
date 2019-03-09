using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {
    
    public GameObject[] obstacles;
    public float startLength = 50;
    public float minLengh = 100;
    public float maxLengh = 200;

    private Transform player;
    private Waypoints points;
    private int targetPointIndex;
    private EvnGenerator evnGenerator;
 
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        points = transform.Find("waypoint").GetComponent<Waypoints>();
        targetPointIndex = points.waypoints.Length - 2;
        evnGenerator = Camera.main.GetComponent<EvnGenerator>();
    }
	// Use this for initialization
	void Start () {
        creatObstacles();
	}
	
	// Update is called once per frame
	void Update () {
	
        //if (transform.position.z + 100 < player.position.z)
        //{
        //    Camera.main.GetComponent<EvnGenerator>().Generate();
         //   GameObject.Destroy(this.gameObject);
        //}
	}
    void creatObstacles()
    {
        float startZ = transform.position.z - 3000;
        float endZ = startZ + 3000;
        float z = startZ + startLength;
        while (true)
        {
            z+=Random.Range(minLengh, maxLengh);
            if (z > endZ)
            {
                break;
            }
            else
            {
                int obsindex=Random.Range(0,obstacles.Length);
                Vector3 pos = getPosZ(z);
                GameObject obs= GameObject.Instantiate(obstacles[obsindex], pos, Quaternion.identity)as GameObject;
                obs.transform.parent = this.transform;
            }
        }
    }
    Vector3 getPosZ(float z)
    {
        Transform[] waypoint = points.waypoints;
        int index=0;
        for(int i = 0; i < waypoint.Length; i++)
        {
            if(z<=waypoint[i].position.z && z >= waypoint[i + 1].position.z)
            {
                index = i;
                break;
            }
        }
        return Vector3.Lerp(waypoint[index + 1].position, waypoint[index].position, (z - waypoint[index + 1].position.z) / (waypoint[index].position.z - waypoint[index + 1].position.z));
    }
    public Vector3 getNextTargetPoint()
    {
        while (true)
        {
            if ((points.waypoints[targetPointIndex].position.z-player.position.z)<5)
        {
            targetPointIndex--;
            if (targetPointIndex<0)
            {
                evnGenerator.Generate();
                GameObject.Destroy(this.gameObject,3);
                return evnGenerator.forest1.getNextTargetPoint();
            }
            }
            else
            {
                return points.waypoints[targetPointIndex].position;
            }
        }
    }
}
