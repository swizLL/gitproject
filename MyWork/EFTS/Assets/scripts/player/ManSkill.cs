using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManSkill : MonoBehaviour {

    public static ManSkill _skill;

    
    public float swordSkillCD = 4f;
    public bool canUseSkill = true;

    private Animator playerAnim;
    private void Awake()
    {
        playerAnim = this.GetComponent<Animator>();
        _skill = this;
    }
    public void swordSkill()
    {
        canUseSkill = false;
        playerAnim.SetBool("useSkill", true);
        Debug.Log("swordSkill");
    }
}
