using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    internal delegate void MyDelegate();
    MyDelegate Shoot;

    private Transform shootPos;//子弹实例化的位置
    private WeaponInfo weaponinfo;//当前武器的武器信息
    private float shootTimer = 10;//射击时间控制器
    private float temperature = 0;//温度
    private float timer = 0;//
    // Use this for initialization
    private void Awake()
    {
        weaponinfo = gameObject.GetComponent<WeaponInfo>();
    }
    void Start()
    {
        shootPos = GameObject.Find("shootPos").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //射击
        shootTimer += Time.deltaTime * weaponinfo.shootSpeed;
        switch (weaponinfo.weaponType)
        {
            case WeaponManager.WeaponType.musket:
                Shoot = Musketshoot;
                break;
            case WeaponManager.WeaponType.AWP:
                Shoot = AWPshoot;
                break;
            case WeaponManager.WeaponType.shotgun:
                Shoot = SGShoot;
                break;
            case WeaponManager.WeaponType.bow:
                Shoot = BowShoot;
                break;
        }
        Shoot();
    }
    //获得子弹信息的方法
    public void getBullet()
    {
        int bulletId = weaponinfo.bulletId;
        WeaponInfo info = WeaponManager._instance.getobjectinfobyid(bulletId);
        string iconmame = info.iconName;
        Sprite buSprite = Resources.Load("Bullet/" + iconmame, typeof(Sprite)) as Sprite;
        bullet.GetComponent<SpriteRenderer>().sprite = buSprite;
        bullet.GetComponent<WeaponInfo>().damage = PlayerControl._player.mainWeapon.GetComponent<WeaponInfo>().damage;
        bullet.GetComponent<WeaponInfo>().critiPoss = PlayerControl._player.mainWeapon.GetComponent<WeaponInfo>().critiPoss;
    }
    //步枪的射击方法
    private void Musketshoot()
    {
        if (temperature < 0) temperature = 0;
        else if (temperature > weaponinfo.hotLimit) temperature = weaponinfo.hotLimit;
        if (Input.GetMouseButton(0) && shootTimer > 1 && temperature < weaponinfo.hotLimit)
        {
            getBullet();
            shootTimer = 0;
            temperature += 1;
            //实例化出来的子弹,朝向与射击位置一致
            if (PlayerControl._player.transform.localScale.x == 1)
            {
                GameObject instbullet = Instantiate(bullet, shootPos.position, shootPos.rotation) as GameObject;
                Destroy(instbullet, weaponinfo.shootDistance/2);
            }
            else
            {
                GameObject instbullet = Instantiate(bullet, shootPos.position, shootPos.rotation) as GameObject;
                instbullet.transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                instbullet.GetComponent<Bullet>().Speed = -instbullet.GetComponent<Bullet>().Speed;
                Destroy(instbullet, weaponinfo.shootDistance/2);
            }
        }
        else if (temperature >= weaponinfo.hotLimit)
        {
            Debug.Log("温度过高!");
        }
        if (!Input.GetMouseButton(0) && temperature > 0)
        {
            temperature -= 10 * Time.deltaTime;
        }
    }
    //狙击枪射击方法
    private void AWPshoot()
    {
        if (Input.GetMouseButtonDown(0) && shootTimer > 1)
        {
            getBullet();
            bullet.GetComponent<Bullet>().Speed = 40;
            shootTimer = 0;
            //实例化出来的子弹,朝向与射击位置一致
            if (PlayerControl._player.transform.localScale.x == 1)
            {
                GameObject instbullet = Instantiate(bullet, shootPos.position, shootPos.rotation) as GameObject;
                Destroy(instbullet, weaponinfo.shootDistance/2);
            }
            else
            {
                GameObject instbullet = Instantiate(bullet, shootPos.position, shootPos.rotation) as GameObject;
                instbullet.transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                instbullet.GetComponent<Bullet>().Speed = -instbullet.GetComponent<Bullet>().Speed;
                Destroy(instbullet, weaponinfo.shootDistance/2);
            }
        }
    }
    //霰弹枪的射击
    private void SGShoot()
    {

        if (Input.GetMouseButtonDown(0) && shootTimer > 1)
        {
            getBullet();
            shootTimer = 0;
            bullet.GetComponent<Bullet>().Speed = 20;
            //实例化出来的子弹,朝向与射击位置一致
            if (PlayerControl._player.transform.localScale.x == 1)
            {
                float i = 0;
                while (i < weaponinfo.sgBulltNum)
                {
                    GameObject instbullet = Instantiate(bullet,shootPos.position, shootPos.rotation) as GameObject;
                    instbullet.transform.Rotate(0, 0, Mathf.Pow(-1, i) * 5 * Mathf.CeilToInt(i / 2));//第二三颗子弹加减五度，第四五颗子弹加减十度，以此类推                  
                    Destroy(instbullet, weaponinfo.shootDistance/2);
                    i++;
                }
            }
            else
            {
                float i = 0;
                while (i < weaponinfo.sgBulltNum)
                {
                    GameObject instbullet = Instantiate(bullet,shootPos.position, shootPos.rotation) as GameObject;
                    instbullet.transform.Rotate(0, 0, Mathf.Pow(-1, i) * 5 * Mathf.CeilToInt(i / 2));
                    instbullet.transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
                    instbullet.GetComponent<Bullet>().Speed = -instbullet.GetComponent<Bullet>().Speed;
                    Destroy(instbullet, weaponinfo.shootDistance/2);
                    i++;
                }
            }
        }
    }
    private void BowShoot()
    {
        getBullet();
        if (Input.GetMouseButton(0) && timer < weaponinfo.bowTime)
        {
            weaponinfo.GetComponent<SpriteRenderer>().sprite = Resources.Load("Weapon/" + weaponinfo.iconName + "_1", typeof(Sprite)) as Sprite;
            timer += Time.deltaTime * 2;
        }
        int power = Mathf.RoundToInt(timer);
        bullet.GetComponent<WeaponInfo>().damage = weaponinfo.damage * power;
        bullet.GetComponent<Bullet>().Speed = 8*(power+1);
        if (Input.GetMouseButtonUp(0))
        {
            timer = 0;
            weaponinfo.GetComponent<SpriteRenderer>().sprite = Resources.Load("Weapon/" + weaponinfo.iconName, typeof(Sprite)) as Sprite;
            if (PlayerControl._player.transform.localScale.x == 1)
            {
                GameObject instbullet = Instantiate(bullet, shootPos.position, shootPos.rotation) as GameObject;
                instbullet.transform.localScale = new Vector3(1.5f, 0.5f, 1f);
                Destroy(instbullet, weaponinfo.shootDistance/2);
            }
            else
            {
                GameObject instbullet = Instantiate(bullet, shootPos.position, shootPos.rotation) as GameObject;
                instbullet.transform.localScale = new Vector3(-1.5f, 0.5f, 1f);
                instbullet.GetComponent<Bullet>().Speed = -instbullet.GetComponent<Bullet>().Speed;
                Destroy(instbullet, weaponinfo.shootDistance/2);
            }
        }
    }
}
