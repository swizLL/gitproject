using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    //战斗按钮
    private Button combatButton;
    private Button knapsackButton;
    private Button taskButton;
    private Button skillButton;
    private Button shopButton;
    private Button systemButton;

    private GameObject taskPanel;
    private GameObject knapsackPanel;
    private GameObject skillPanel;
    private void Awake()
    {
        combatButton = FindButton("Combat");
        combatButton.onClick.AddListener(onCombat);
        knapsackButton = FindButton("Bag");
        knapsackButton.onClick.AddListener(onKnapsack);
        taskButton = FindButton("Task");
        taskButton.onClick.AddListener(onTask);
        skillButton = FindButton("Skill");
        skillButton.onClick.AddListener(onSkill);
        shopButton = FindButton("Shop");
        shopButton.onClick.AddListener(onShop);
        systemButton = FindButton("System");
        systemButton.onClick.AddListener(onSystem);

        taskPanel = GameObject.Find("UI/Panel/TaskUI").gameObject;
        knapsackPanel = GameObject.Find("UI/Panel/Knapsack").gameObject;
        skillPanel = GameObject.Find("UI/Panel/SkillUI").gameObject;
    }
    Button FindButton(string btnName)
    {
        return transform.Find(btnName).GetComponent<Button>();

    }
    void onCombat()
    {

    }
    void onKnapsack()
    {
        if (knapsackPanel.GetComponent<RectTransform>().anchoredPosition.x >= 800)
        {
            Knaspack._intance.onOpen();
        }
        else
        {
            Knaspack._intance.onClose();
        }

    }
    void onTask()
    {
        if (taskPanel.GetComponent<RectTransform>().anchoredPosition.x >= 800)
        {
            TaskUI._instance.onOpen();
        }
        else
        {
            TaskUI._instance.onClose();
        }
    }
    void onSkill()
    {
        if (skillPanel.GetComponent<RectTransform>().anchoredPosition.x >= 800)
        {
            SkillUI._instance.Show();
        }
        else
        {
            SkillUI._instance.Close();
        }
    }
    void onShop()
    {

    }
    void onSystem()
    {

    }
}

