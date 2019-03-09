using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskUI : MonoBehaviour
{
    public static TaskUI _instance;

    private GameObject taskListGrid;
    private Button closeBtn;

    public GameObject taskItemPrefab;

    private void Awake()
    {
        _instance = this;
        taskListGrid = transform.Find("ScrollPanel/ViewPanel/Content").gameObject;
        closeBtn = transform.Find("ButtonClose").GetComponent<Button>();
        closeBtn.onClick.AddListener(onClose);
    }
    private void Start()
    {
        InitTaskListUI();
    }
    public void InitTaskListUI()
    {
        //获取prefab的高度
        float itemHeight = taskItemPrefab.GetComponent<RectTransform>().rect.height;
        //获取当前content组件的高度
        Vector2 contentSize = taskListGrid.GetComponent<RectTransform>().sizeDelta;
        ArrayList taskList = TaskManager._instance.GetTaskList();
        if (contentSize.y <= taskList.Count * itemHeight)
        {
            //因为设置了layout组件的每一个元素之间的上下之间都相隔5，所以要加上这个间隔
            taskListGrid.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.x, taskList.Count * itemHeight + (taskList.Count + 1) * 5);
        }
        foreach (Task task in taskList)
        {
            GameObject go = Instantiate(taskItemPrefab, taskListGrid.transform);
            go.GetComponent<TaskItemUI>().setTask(task);
        }
    }
    public void onClose()
    {
        this.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(1200, 0);
    }
    public void onOpen()
    {
        this.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}
