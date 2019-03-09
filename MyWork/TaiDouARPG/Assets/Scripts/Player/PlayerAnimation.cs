using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public static PlayerAnimation _instance;
    private Animator anim;
    private void Awake()
    {
        _instance = this;
        anim = this.GetComponent<Animator>();
    }
    private void Update()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1);
        if (info.normalizedTime == 1.0f && info.IsName("Skill01"))
        {
            anim.SetBool("Skill01", true);
        }
    }
    public void OnAttackButtonClick(SkillPos skillPos)
    {
        if (skillPos == SkillPos.Basic)
        {
            anim.SetTrigger("Attack");
        }
        else
        {
            switch (skillPos)
            {
                case SkillPos.One:
                    anim.SetTrigger("Skill01");
                    break;
                case SkillPos.Two:
                    anim.SetTrigger("Skill02");
                    break;
                case SkillPos.Three:
                    anim.SetTrigger("Skill03");
                    break;
            }
        }
    }
}
