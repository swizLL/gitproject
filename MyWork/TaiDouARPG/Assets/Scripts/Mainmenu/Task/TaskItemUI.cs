using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskItemUI : MonoBehaviour
{
    private Image taskTypeImage;
    private Image taskIconImage;
    private Text taskNameTxt;
    private Text taskDesTxt;
    private Image reward1Image;
    private Text reward1Value;
    private Image reward2Image;
    private Text reward2Value;
    private Button rewardBtn;
    private Button battleBtn;
    private Text battleBtnTxt;

    private Task task;
    private void Awake()
    {
        taskTypeImage = transform.Find("taskTypeImg").GetComponent<Image>();
        taskIconImage = transform.Find("taskIconBG/taskIcon").GetComponent<Image>();
        taskNameTxt = transform.Find("taskName").GetComponent<Text>();
        taskDesTxt = transform.Find("taskDes").GetComponent<Text>();
        reward1Image = transform.Find("taskReward/Coin").GetComponent<Image>();
        reward1Value = transform.Find("taskReward/Coin/Value").GetComponent<Text>();
        reward2Image = transform.Find("taskReward/Diamon").GetComponent<Image>();
        reward2Value = transform.Find("taskReward/Diamon/Value").GetComponent<Text>();
        rewardBtn = transform.Find("getRewardBtn").GetComponent<Button>();
        rewardBtn.onClick.AddListener(onReward);
        battleBtn = transform.Find("battleBtn").GetComponent<Button>();
        battleBtn.onClick.AddListener(onBattle);
        battleBtnTxt = transform.Find("battleBtn/Text").GetComponent<Text>();
    }

    public void setTask(Task task)
    {
        this.task = task;
        task.onTaskChange += this.onTaskChange;
        updateTaskUI();
    }
    private void updateTaskUI()
    {
        switch (task.TaskType)
        {
            case TaskType.Main:
                taskTypeImage.sprite = Resources.Load("Task/pic_主线", typeof(Sprite)) as Sprite;
                break;
            case TaskType.Reward:
                taskTypeImage.sprite = Resources.Load("Task/pic_奖赏", typeof(Sprite)) as Sprite;
                break;
            case TaskType.Daily:
                taskTypeImage.sprite = Resources.Load("Task/pic_日常", typeof(Sprite)) as Sprite;
                break;
        }
        taskIconImage.sprite = Resources.Load("Task/" + task.Icon, typeof(Sprite)) as Sprite;
        taskNameTxt.text = task.Name;
        taskDesTxt.text = task.Des;
        if (task.Coin > 0 && task.Diamond > 0)
        {
            reward1Image.sprite = Resources.Load("Task/金币", typeof(Sprite)) as Sprite;
            reward1Value.text = "x" + task.Coin;
            reward2Image.sprite = Resources.Load("Task/钻石", typeof(Sprite)) as Sprite;
            reward2Value.text = "x" + task.Diamond;
        }
        else if (task.Coin > 0 && task.Diamond <= 0)
        {
            reward1Image.sprite = Resources.Load("Task/金币", typeof(Sprite)) as Sprite;
            reward1Value.text = "x" + task.Coin;
            reward2Image.gameObject.SetActive(false);
            reward2Value.gameObject.SetActive(false);
        }
        else if (task.Coin <= 0 && task.Diamond > 0)
        {
            reward1Image.sprite = Resources.Load("Task/钻石", typeof(Sprite)) as Sprite;
            reward1Value.text = "x" + task.Coin;
            reward2Image.gameObject.SetActive(false);
            reward2Value.gameObject.SetActive(false);
        }
        switch (task.TaskProgress)
        {
            case TaskProgress.NotStart:
                rewardBtn.gameObject.SetActive(false);
                battleBtnTxt.text = "接受任务";
                battleBtn.gameObject.SetActive(true);
                break;
            case TaskProgress.Accept:
                rewardBtn.gameObject.SetActive(false);
                battleBtnTxt.text = "下一步";
                battleBtn.gameObject.SetActive(true);
                break;
            case TaskProgress.Complete:
                rewardBtn.gameObject.SetActive(true);
                battleBtn.gameObject.SetActive(false);
                break;
        }
    }
    public void onBattle()
    {
        TaskManager._instance.OnExcuteTask(task);
    }
    public void onReward()
    {
        TaskManager._instance.OnExcuteTask(task);
    }
    private void onTaskChange()
    {
        updateTaskUI();
    }
}
