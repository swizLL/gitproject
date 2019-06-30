using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float Speed = 1;
    public int TotalHP=200;//总血量
    public int HP;//当前血量
    public int attackRate = 2;//攻击速率，表示多少秒攻击一次
    public float attackRange = 2;
    public int Damage=20;//攻击力

    private float distance;
    private float attackTimer = 0;
    private Animation anim;
    private Transform bloodPoint;
    public GameObject damageEffect;
    private CharacterController cc;
    private Slider BloodBar;
    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        bloodPoint = this.transform.Find("BloodPoint").transform;
        cc = this.GetComponent<CharacterController>();
        HP = TotalHP;
    }
    private void Start()
    {
        TranscriptManager._instance.enemyList.Add(this.gameObject);
        InvokeRepeating("GetDistance", 0, 0.1f);
        BloodBar = HPBarManager._instance.getBloodBar().GetComponent<Slider>();
    }
    //移动的方法
    private void Update()
    {
        if (HP <= 0)
        {
            transform.Translate(-transform.up * Time.deltaTime);
            return;
        }
        //攻击
        if (distance < attackRange)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackRate)
            {
                //进行攻击
                anim.Play("attack01");
                attackTimer = 0;
            }
            if (!anim.IsPlaying("attack01"))
            {
                anim.CrossFade("idle");
            }
        }
        else if (!anim.IsPlaying("takedamage"))
        {
            Move();
        }
        bloodBarFollow();
    }
    void Attack()
    {
        GetDistance();
        if (distance < attackRange)
        {
            //最后那个参数是为了使主角身上没有这个方法时不抛出异常
            TranscriptManager._instance.player.SendMessage("TakeDamage",Damage,SendMessageOptions.DontRequireReceiver);
        }
    }
    void Move()
    {
        Transform player = TranscriptManager._instance.player.transform;
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        anim.Play("walk");
        cc.SimpleMove(transform.forward * Speed);
    }
    void GetDistance()
    {
        Transform player = TranscriptManager._instance.player.transform;
        distance = Vector3.Distance(player.position, transform.position);
    }
    //收到攻击时调用这个方法
    //收到多少伤害
    //浮空和后退的距离
    void takeDamage(string args)
    {
        if (HP <= 0) return;
        string[] proArray = args.Split(',');
        //生命减少
        int damage = int.Parse(proArray[0]);
        HP -= damage;
        //伤害显示
        DamageTextManager._instance.InstantiateDamageText(this.gameObject, damage);
        //血条更新
        BloodBar.value = (float)HP / TotalHP;
        //受到攻击的动画
        anim.Play("takedamage");
        //浮空和后退
        float backDistance = float.Parse(proArray[1]);
        float upDistance = float.Parse(proArray[2]);
        string[] layerArray = { "Wall", "Ground" };
        //射线检测敌人后面的墙或者台阶,若如果后面有台阶，就只能击飞，如果后面没有台阶，就可以被击退或者击飞
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position, TranscriptManager._instance.player.transform.forward, out hit, 2f, LayerMask.GetMask(layerArray));
        if (!ray)
        {
            iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(TranscriptManager._instance.player.transform.forward) * backDistance + Vector3.up * upDistance, 0.3f);
        }
        else
        {
            iTween.MoveBy(this.gameObject, Vector3.up * upDistance, 0.3f);
        }
        //出血特效
        GameObject blood = Instantiate(damageEffect, bloodPoint.position, Quaternion.identity, bloodPoint);
        if (HP <= 0)
        {
            Dead();
        }
    }
    //收到火鸟和恶魔之爪攻击的方法
    //伤害
    //音效名字
    //浮空效果
    void takeEffectDmage(string args)
    {
        if (HP <= 0) return;
        string[] proArray = args.Split(',');
        //生命减少
        int damage = int.Parse(proArray[0]);
        HP -= damage;
        //伤害显示
        DamageTextManager._instance.InstantiateDamageText(this.gameObject, damage);
        //血条更新
        BloodBar.value = (float)HP / TotalHP;
        string audioName = proArray[1];
        float upDistance = float.Parse(proArray[2]);
        SoundManager._instance.playerSound(audioName);
        iTween.MoveBy(this.gameObject, Vector3.up * upDistance, 0.3f);
        if (HP <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        int rand = Random.Range(0, 10);
        if (rand <= 7)
        {
            anim.Play("die");
        }
        else
        {
            anim.Play("die");
            this.GetComponentInChildren<MeshExploder>().Explode();
        }
        TranscriptManager._instance.enemyList.Remove(this.gameObject);
        cc.enabled = false;
        Destroy(BloodBar.gameObject);
        Destroy(this.gameObject, 5);
    }
    //Destroy本身就有自带延迟的参数，所以不需要使用协程了
    //public IEnumerator DestroyObj(GameObject obj,float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Destroy(obj);
    //}
    //血条跟随
    void bloodBarFollow()
    {
        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        //0和100是偏移量
        BloodBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(vec2.x - Screen.width / 2 + 0, vec2.y - Screen.height / 2 + 110);
        //超出屏幕范围外便不显示
        if (vec2.x > Screen.width || vec2.x < 0 || vec2.y > Screen.height || vec2.y < 0)
        {
            BloodBar.gameObject.SetActive(false);
        }
        else
        {
            BloodBar.gameObject.SetActive(true);
        }
    }
}
