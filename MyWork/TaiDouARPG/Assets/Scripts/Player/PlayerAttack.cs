using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();
    public PlayerEffect[] effectArray;
    //向前方的攻击距离
    public float forwarAttackDis = 2;
    //周围的攻击距离
    public float aroundAttackDis = 2;
    //伤害数组
    public int[] damageArray = new int[] { 20, 30, 30, 30 };
    public enum AttackRange
    {
        Forward,
        Around
    }
    private void Start()
    {
        PlayerEffect[] peArray = this.GetComponentsInChildren<PlayerEffect>();
        foreach (PlayerEffect pe in peArray)
        {
            effectDict.Add(pe.gameObject.name, pe);
        }
        foreach (PlayerEffect pe in effectArray)
        {
            effectDict.Add(pe.gameObject.name, pe);
        }
    }
    //
    //0 bacic skill1 skill2 skill3
    //1 effect name
    //2 sound name
    //3 move forward 击退以及前冲的距离
    //4 jump height 击飞高度
    public void Attack(string args)
    {
        string[] proArray = args.Split(',');
        //effect name
        string effectName = proArray[1];
        showPlayerEffect(effectName);
        //sound name
        string soundName = proArray[2];
        SoundManager._instance.playerSound(soundName);
        //move forward
        float moveForward = float.Parse(proArray[3]);
        //射线检测前方是不是墙或者台阶
        string[] layerArray = { "Wall", "Ground" };
        RaycastHit hit;
        bool ray = Physics.Raycast(transform.position, transform.forward, out hit, 2f, LayerMask.GetMask(layerArray));
        if (moveForward > 0.1f&&!ray)
        {
            iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);
            //rigidbody.AddForce(Vector3.forward * moveForward*100);
        }
        string attackType = proArray[0];
        if (attackType == "normal")
        {
            ArrayList array = getEnemyInAttckRange(AttackRange.Forward);
            foreach(GameObject go in array)
            {
                go.SendMessage("takeDamage",damageArray[0]+","+proArray[3]+","+proArray[4]);//TODO
            }
        }
    }
    void showPlayerEffect(string effectName)
    {
        PlayerEffect pe;
        if (effectDict.TryGetValue(effectName, out pe))
        {
            pe.Show();
        }
    }
    void ShowEffectDevHand()
    {
        string effectName = "DevilHandMobile";
        PlayerEffect pe;
        if(effectDict.TryGetValue(effectName, out pe))
        {
            ArrayList array = getEnemyInAttckRange(AttackRange.Forward);
            foreach (GameObject go in array)
            {
                RaycastHit hit;
                bool ray = Physics.Raycast(go.transform.position+Vector3.up, Vector3.down, out hit, 10f, LayerMask.GetMask("Ground"));
                if (ray)
                {
                    GameObject.Instantiate(pe, hit.point, Quaternion.identity);
                }
            }
        }
    }
    //得到攻击范围内的敌人
    ArrayList getEnemyInAttckRange(AttackRange attackRange)
    {
        ArrayList arrayList = new ArrayList();
        if (attackRange == AttackRange.Forward)
        {
            foreach (GameObject go in TranscriptManager._instance.enemyList)
            {
                //将世界坐标转化为局部坐标
                Vector3 pos = transform.InverseTransformPoint(go.transform.position);
                if (pos.z > 0.5f)
                {
                    float distance = Vector3.Distance(Vector3.zero, pos);
                    if (distance < forwarAttackDis)
                    {
                        arrayList.Add(go);
                    }
                }
            }
        }
        else
        {
            foreach (GameObject go in TranscriptManager._instance.enemyList)
            {
                float distance = Vector3.Distance(transform.position,go.transform.position);
                if (distance < aroundAttackDis)
                {
                    arrayList.Add(go);
                }
            }
        }
        return arrayList;
    }
}
