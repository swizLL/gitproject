using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TaskType
{
    Main,//主线任务
    Reward,//赏金任务
    Daily//日常任务
}
public enum TaskProgress
{
    NotStart,
    Accept,
    Complete,
    Reward
}
public class Task
{/*
    ID
    任务类型(Main,Reward,Daily)
    名称
    图标
    任务描述
    获得的金币奖励
    获得的钻石奖励
    跟npc交谈的话语
    npc的id
    副本id
    人物的状态
    未开始
    接受任务
    任务完成
    获取奖励(结束)
 */
    private int id;
    private TaskType taskType;
    private string name;
    private string icon;
    private string des;
    private int coin;
    private int diamond;
    private string npcTalk;
    private int npcID;
    private int transcriptID;
    private TaskProgress taskProgress = TaskProgress.NotStart;

    public delegate void onTaskChangedEvent();
    public event onTaskChangedEvent onTaskChange;
    #region GETSET
    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public TaskType TaskType
    {
        get
        {
            return taskType;
        }

        set
        {
            taskType = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Icon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public string Des
    {
        get
        {
            return des;
        }

        set
        {
            des = value;
        }
    }

    public int Coin
    {
        get
        {
            return coin;
        }

        set
        {
            coin = value;
        }
    }

    public int Diamond
    {
        get
        {
            return diamond;
        }

        set
        {
            diamond = value;
        }
    }

    public string NpcTalk
    {
        get
        {
            return npcTalk;
        }

        set
        {
            npcTalk = value;
        }
    }

    public int NpcID
    {
        get
        {
            return npcID;
        }

        set
        {
            npcID = value;
        }
    }

    public int TranscriptID
    {
        get
        {
            return transcriptID;
        }

        set
        {
            transcriptID = value;
        }
    }

    public TaskProgress TaskProgress
    {
        get
        {
            return taskProgress;
        }

        set
        {
            if (TaskProgress != value)
            {
                taskProgress = value;
                onTaskChange();
            }
        }
    }
    #endregion
}
