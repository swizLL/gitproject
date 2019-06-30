using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour {
    public static TaskManager _instance;
    public TextAsset taskInfoText;
    private ArrayList taskList = new ArrayList();
    private Task currentTask;
    private PlayerAutoMove playerAutoMove;
    AsyncOperation async = null;
    private PlayerAutoMove PlayerAutoMove
    {
        get
        {
            if (playerAutoMove == null)
            {
                playerAutoMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
            }
            return playerAutoMove;
        }
    }

    ///<summary>
    ///初始化任务信息
    ///</summary>
    private void Awake()
    {
        _instance = this;
        InitTask();
    }
    public void InitTask()
    {
        string[] taskInfoArray = taskInfoText.ToString().Split('\n');
        foreach(string str in taskInfoArray)
        {
            string[] proArray= str.Split('|');
            Task task = new Task();
            task.Id = int.Parse(proArray[0]);
            switch(proArray[1])
            {
                case "Main":
                    task.TaskType = TaskType.Main;
                    break;
                case "Reward":
                    task.TaskType = TaskType.Reward;
                    break;
                case "Daily":
                    task.TaskType = TaskType.Daily;
                    break;
            }
            task.Name = proArray[2];
            task.Icon = proArray[3];
            task.Des = proArray[4];
            task.Coin = int.Parse(proArray[5]);
            task.Diamond = int.Parse(proArray[6]);
            task.NpcTalk = proArray[7];
            task.NpcID =int.Parse(proArray[8]) ;
            task.TranscriptID = int.Parse(proArray[9]);
            taskList.Add(task);
        }
    }
    public ArrayList GetTaskList()
    {
        return taskList;
    }
    //接受某个任务
    public void OnExcuteTask(Task task)
    {
        this.currentTask = task;
        switch (task.TaskProgress)
        {
            case TaskProgress.NotStart:
                PlayerAutoMove.setTargetPosition(NPCManager._instance.getNpcById(task.NpcID).transform.position);
                TaskUI._instance.onClose();
                break;
            case TaskProgress.Accept:
                PlayerAutoMove.setTargetPosition(NPCManager._instance.transcriptGo.transform.position);
                TaskUI._instance.onClose();
                break;
        }
    }
    //接受任务后调用的方法
    public void onAcceptTask()
    {
        currentTask.TaskProgress = TaskProgress.Accept;
        //TODO自动寻路到副本
        PlayerAutoMove.setTargetPosition(NPCManager._instance.transcriptGo.transform.position);
    }
    //自动导航到达地点后调用的方法，可以根据当前任务的进程来判断是到达npc面前还是到达副本面前
    public void onArrivePos()
    {
        //到达npc面前
        if(currentTask.TaskProgress==TaskProgress.NotStart)
        {
            NpcDialogUI._instance.onShow(currentTask.NpcTalk);
        }
        //TODO,到达副本面前
        if(currentTask.TaskProgress==TaskProgress.Accept)
        {
            StartCoroutine("Loading");
        }
    }
    IEnumerator Loading()
    {
        async = SceneManager.LoadSceneAsync("transcript");
        async.allowSceneActivation = false;
        LoadSceneProgressBar._instance.Show(async);
        while(!async.isDone)
        {
            if (async.progress >= 0.9f)
                break;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        async.allowSceneActivation = true;
    }
}
