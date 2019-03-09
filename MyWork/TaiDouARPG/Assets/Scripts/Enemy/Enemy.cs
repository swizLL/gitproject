﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animation anim;
    public int HP = 200;
    private Transform bloodPoint;
    public GameObject damageEffect;
    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        bloodPoint = this.transform.Find("BloodPoint").transform;
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
        //射线检测敌人后面的墙或者台阶
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position, TranscriptManager._instance.player.transform.forward, out hit, 2f, LayerMask.GetMask(layerArray));
        if (!ray)
        {
            iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(TranscriptManager._instance.player.transform.forward) * backDistance + Vector3.up * upDistance, 0.3f);
        }
        //出血特效
        GameObject blood = Instantiate(damageEffect, bloodPoint.position, Quaternion.identity, bloodPoint);
        if (HP <= 0)
        {
            Dead();
        }
    }
    void Dead()
    {
        anim.Play("die");
    }
}