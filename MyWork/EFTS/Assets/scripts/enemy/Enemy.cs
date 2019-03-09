using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   public enum damageType//攻击类型
    {
        Nomal,//普通
        Critical//暴击伤害
    }
public class Enemy : MonoBehaviour {
    public float hp = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(hp<=0)
        {
            Destroy(this.gameObject);
        }
		
	}
    public void beAttack(float damage,damageType dmgType)
    {
        //float possibility = Random.value;
       if(dmgType==damageType.Nomal)
        {
            Debug.Log("Nomal");
            hp -= damage;
        }
        else
        {
            Debug.Log("Critical");
            hp -= damage * 2f;
        }
       Debug.Log(hp);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == Tags.bullet)
        {
            bool isCritical = Random.value < collision.GetComponent<WeaponInfo>().critiPoss;
            damageType damgType;
            if (!isCritical)
            {
                damgType = damageType.Nomal;
            }else
            {
                damgType = damageType.Critical;
            }
            beAttack(collision.GetComponent<WeaponInfo>().damage,damgType);
        }
    }
}
