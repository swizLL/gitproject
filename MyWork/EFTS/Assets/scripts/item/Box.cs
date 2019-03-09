using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Box : MonoBehaviour {
    public GameObject weaponPrefab;

    private Sprite openSprite;
    private bool canopen = true;
    private void Awake()
    {
        openSprite= Resources.Load("boxOpen", typeof(Sprite)) as Sprite;
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tags.player)
        {
            if (Input.GetKeyDown(KeyCode.E)&&canopen)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
                getWeapon();
                canopen = false;
            }
        }
    }
    private void getWeapon()
    {
        int itemId = ItemInstManager._instance.GetItemId();
        WeaponInfo weapon = WeaponManager._instance.getobjectinfobyid(itemId);
        weaponPrefab.GetComponent<WeaponInfo>().InfoCopy(weapon);
        weaponPrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load("Weapon/" + weapon.iconName, typeof(Sprite)) as Sprite;
        Instantiate(weaponPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }
}
