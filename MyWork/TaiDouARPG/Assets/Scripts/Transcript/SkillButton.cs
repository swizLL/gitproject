using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillPos skillPos = SkillPos.Basic;
    private PlayerAnimation playerAnimation;
    public float ColdTime = 4;
    private float ColdTimer = 0;//剩余的冷却时间
    private Image cdImage;
    private void Start()
    {
        playerAnimation = TranscriptManager._instance.player.GetComponent<PlayerAnimation>();
        this.GetComponent<Button>().onClick.AddListener(onClick);
        if (transform.Find("CD"))
        {
            cdImage = transform.Find("CD").GetComponent<Image>();
        }
    }
    private void Update()
    {
        if (cdImage == null) return;
        if (ColdTimer > 0)
        {
            ColdTimer -= Time.deltaTime;
            cdImage.fillAmount = ColdTimer / ColdTime;
            if (ColdTimer <= 0)
            {
                this.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            cdImage.fillAmount = 0;
        }
    }
    void onClick()
    {
        PlayerAnimation._instance.OnAttackButtonClick(skillPos);
        if (cdImage != null)
        {
            ColdTimer = ColdTime;
            this.GetComponent<Button>().interactable = false;
        }
    }
}
