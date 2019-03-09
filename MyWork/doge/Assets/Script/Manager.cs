using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    static public Manager m;
    public Transform player;
    public GameObject enemy;
    public GameObject bone;
    public float rateTime = 2;
    public float hurntTime = 1;

    private float myTime;
    private float timer;
    private float damage = 0;
    public int point = 0;
    public float hurnt = 3;
    // Use this for initialization
    void Awake()
    {
        m = this;
    }
    void Start()
    {
        enemy.GetComponent<jiangshimove>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        myTime += Time.deltaTime;
        timer += Time.deltaTime;
        hurntPlayer();
        instantiateEnemy();
        highLevel();
    }
    void instantiateEnemy()
    {
        if (myTime > rateTime)
        {
            //Vector2 r = Random.insideUnitCircle.normalized * 40;
            damage += 1;
            GameObject jiangshi = GameObject.Instantiate(enemy, new Vector3(Random.Range(-28, 22.5f), 0, Random.Range(-10, 41)), Quaternion.Euler(new Vector3(0, Random.Range(0.0f, 360.0f), 0))) as GameObject;
            jiangshi.transform.SetParent(this.transform);
            myTime -= rateTime;
        }
    }
    void hurntPlayer()
    {
        if (timer > hurntTime)
        {
            player.GetComponent<dogemove>().hp -= hurnt;
            timer -= hurntTime;
        }
    }
    void highLevel()
    {
        if (damage >= 30)
        {
            rateTime = 1.5f;
            bone.GetComponent<bone>().rehealth = 8;
            hurnt = 5;
        }
    }
}
