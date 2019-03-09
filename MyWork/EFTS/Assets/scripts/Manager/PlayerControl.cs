using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D PlayerRigid;
    private bool isGround = true;
    private bool canTwiceJump = false;
    private bool canMove=true;
    
    public  WeaponInfo mainWeapon;//主武器
    public  WeaponInfo offWeapon;//副武器
    public  GameObject attackCollider;//攻击碰撞
    public float jumpForce=300;
    public float speed = 5;
    public Texture2D zhunxing;//准星
    public static PlayerControl _player;
    private void Awake()
    {
        _player = this;   
    }
    void Start ()
    { 
        //初始武器
        mainWeapon.InfoCopy(WeaponManager._instance.getobjectinfobyid(1005));
        mainWeapon.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Weapon/" + mainWeapon.iconName, typeof(Sprite)) as Sprite;
        anim = GetComponent<Animator>();
        PlayerRigid = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Move();
        //Attack();
        //状态控制
        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.IsName("idle"))
        {
            canMove = true;
        }
        else if (animatorInfo.IsName("attack") || animatorInfo.IsName("skill"))
        {
            canMove = false;
            attackCollider.SetActive(true);
        }
        if (!animatorInfo.IsName("attack") && !animatorInfo.IsName("skill"))
            attackCollider.SetActive(false);
        Vector3 manPos = new Vector3(Screen.width/2, Screen.height/2, 0);//人物在鼠标坐标系下的位置
        Vector3 shootDir = Input.mousePosition - manPos;//目标坐标与当前坐标差的向量
        float yChazhi = Input.mousePosition.y - manPos.y;
        if (0 == yChazhi) yChazhi = 0.0001f;//防止除数为零 
        float angle = (yChazhi / Mathf.Abs(yChazhi)) * Vector3.Angle(transform.right, shootDir); // 返回当前坐标与目标坐标的角度
        if (transform.localScale.x > 0)
            GameObject.Find("Weapon").transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        else if (transform.localScale.x < 0)
            GameObject.Find("Weapon").transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180 - angle));  
    }
    public void Move()
    {
        //跳跃
        if (Input.GetKeyDown(KeyCode.Space)&& isGround&&canMove)
        {
            isGround = false;
            canTwiceJump = true;
            anim.SetTrigger("Jump");
            PlayerRigid.AddForce(Vector2.up * jumpForce);
            anim.SetFloat("jumpTime", 1);
        }
        else if(!isGround&& Input.GetKeyDown(KeyCode.Space)&&canTwiceJump)
        {
            canTwiceJump = false;
            anim.SetTrigger("twiceJump");
            PlayerRigid.AddForce(Vector2.up * jumpForce/2);
            anim.SetFloat("jumpTime", 1);
        }
        //移动
        int screenZeroPointX = Screen.width / 2;
        float h = Input.GetAxis("Horizontal");
        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        //朝向控制
        if(Input.mousePosition.x < screenZeroPointX)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //动作控制        
        if (h < -0.01f && canMove && Input.mousePosition.x < screenZeroPointX)
        {
            if (animatorInfo.IsName("idle"))
            {
                anim.SetBool("Walk", true);
            }else if(animatorInfo.IsName("backwalk"))
            {
                anim.SetBool("backwalk", false);
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if(h < -0.01f && canMove && Input.mousePosition.x > screenZeroPointX)
        {
            if (animatorInfo.IsName("idle"))
            {
                anim.SetBool("backwalk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (h > 0.01f && canMove&& Input.mousePosition.x > screenZeroPointX)
        {
            if (animatorInfo.IsName("idle"))
            {
                anim.SetBool("Walk", true);
            }
            else if (animatorInfo.IsName("backwalk"))
            {
                anim.SetBool("backwalk", false);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (h >0.01f && canMove && Input.mousePosition.x <screenZeroPointX)
        {
            if (animatorInfo.IsName("idle"))
            {
                anim.SetBool("backwalk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("backwalk", false);
        }
            
    }
    public  void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
            anim.SetFloat("attackTime", 0.2f);
            attackCollider.SetActive(true);
        }
    }

    //捡起武器
    public void GetWeapon(WeaponInfo floorWeapon)//地上的武器
    {
        exchangeWeaponInfo(mainWeapon,floorWeapon);  
    }
    public void exchangeWeaponInfo(WeaponInfo weapon1,WeaponInfo weapon2)
    {
        WeaponInfo info = new WeaponInfo();
        info.InfoCopy(weapon2);
        weapon2.InfoCopy(weapon1);
        weapon1.InfoCopy(info);
        weapon1.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Weapon/" + weapon1.iconName, typeof(Sprite)) as Sprite;
        weapon2.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Weapon/" + weapon2.iconName, typeof(Sprite)) as Sprite;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag==Tags.ground)
        {
            isGround = true;
            anim.SetTrigger("backIdle");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag==Tags.weapon&&Input.GetKeyDown(KeyCode.E))
        {
            GetWeapon(collision.gameObject.GetComponent<WeaponInfo>());
        }
    }
    void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;
        Rect pos = new Rect(mousePos.x - zhunxing.width * 0.5f, Screen.height - mousePos.y - zhunxing.height * 0.5f,zhunxing.width*0.5f, zhunxing.height*0.5f);
        GUI.DrawTexture(pos, zhunxing);
    }
}
