using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1;
    public int HP = 200;

    private Animation anim;
    private Transform bloodPoint;
    public GameObject damageEffect;
    private CharacterController cc;
    private bool canMove = true;
    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        bloodPoint = this.transform.Find("BloodPoint").transform;
        cc = this.GetComponent<CharacterController>();

    }
    //移动的方法
    private void Update()
    {
        if (anim.IsPlaying("takedamage") || HP <= 0)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        if (canMove)
        {
            Move();
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
        anim.Play("die");
        cc.enabled = false;
        canMove = false;
    }
}
