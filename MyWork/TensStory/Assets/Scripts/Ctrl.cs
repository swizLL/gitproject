using UnityEngine;
using System.Collections;
using GDGeek;
using System.Collections.Generic;
public class Ctrl : MonoBehaviour
{
    private FSM fsm_ = new FSM();
    public View _view = null;
    public Model _model = null;
    public Sound _sound = null;
    public int score = 0;
    public void fsmPost(string msg)
    {
        fsm_.post(msg);
    }
    public void refreshModel2View()
    {
        for (int x = 0; x < _model.width; ++x)
        {
            for (int y = 0; y < _model.height; ++y)
            {
                Square s = _view.play.getSquare(x, y);
                Cube c = _model.getCube(x, y);
                if (c.isEnable)
                {
                    s.number = c.number;
                    s.show();
                }
                else
                {
                    s.hide();
                }
            }
        }
    }
    State beginState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate
        {
            _view.begin.gameObject.SetActive(true);
            foreach(Cube c in _model.list)
            {
                c.isEnable = false;
            }
            this.score = 0;
            _sound.begin.Play();
        };
        state.onOver += delegate
        {
            _view.begin.gameObject.SetActive(false);
            _sound.begin.Stop();
        };
        state.addEvent("toplay", "input");
        return state;
    }
    State playState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate
        {
            _view.play.gameObject.SetActive(true);
            refreshModel2View();
            //Square square = _view.play.getSquare(0,1);
            //square.number = 7;
            //square.show();
            //Square square2 = _view.play.getSquare(2,3);            
            //square2.number = 54;
            //square2.show();
        };
        state.onOver += delegate
        {
            _view.play.gameObject.SetActive(false);
        };
        state.addEvent("GameOver", "end");
        return state;
    }
    State endState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate
        {
            _sound.end.Play();
            _view.end.gameObject.SetActive(true);
        };
        state.onOver += delegate
        {
            _view.end.gameObject.SetActive(false);
        };
        state.addEvent("again", "begin");
        return state;
    }
    private void input(int x, int num)
    {
        Cube c = _model.getCube(0, 0);
        c.isEnable = false;
        c = _model.getCube(x, 0);
        c.isEnable = true;
        c.number = num;
        //for (int y = _model.height - 1; y >= 0; --y)
        //{
        //    Cube c = _model.getCube(x, y);
        //    if (!c.isEnable)
        //    {
        //        c.isEnable = true;
        //        
        //        break;
        //    }
        //}
        refreshModel2View();
    }
    State inputState()
    {
        StateWithEventMap state = new StateWithEventMap();
        int _number = 0;
        state.onStart += delegate
        {
            if (score <= 150) {_number = Random.Range(3, 8); }
            if (score > 150 && score <=300) { _number = Random.Range(2, 9); }
            if (score > 300) { _number = Random.Range(1, 10); }
            Cube cc = _model.getCube(0, 0);
            cc.number = _number;
            cc.isEnable = true;
            refreshModel2View();
            //Debug.LogWarning("in input!");
        };
        state.addAction("1", delegate (FSMEvent evt)
        {
            //Debug.Log("I get one~");
            input(0, _number);
            return "fall";
        });
        state.addAction("2", delegate (FSMEvent evt)
        {
           // Debug.Log("I get two~");
            input(1, _number);
            return "fall";
        });
        state.addAction("3", delegate (FSMEvent evt)
        {
            //Debug.Log("I get three~");
            input(2, _number);
            return "fall";
        });
        state.addAction("4", delegate (FSMEvent evt)
        {
            //Debug.Log("I get four~");
            input(3, _number);
            return "fall";
        });
        return state;
    }
    private Task doFall()
    {
        TaskSet ts = new TaskSet();
        for (int x = 0; x < _model.width; ++x)
        {
            for (int y = _model.height - 1; y >= 0; --y)
            {
                Cube c = _model.getCube(x, y);
                Cube end = null;
                int endY = 0;
                if (c.isEnable)
                {
                    for (int n = y + 1; n < _model.height; ++n)
                    {
                        Cube next = _model.getCube(x, n);
                        if (next == null || next.isEnable == true)
                        {
                            break;
                        }
                        else
                        {
                            end = next;
                            endY = n;
                        }
                    }
                        if (end != null)
                        {
                            end.number = c.number;
                            end.isEnable = true;
                            c.isEnable = false;                                                    
                            ts.push(_view.play.moveTask(c.number, new Vector2(x, y), new Vector2(x, endY)));
                        }                   
                }
            }
        }
        TaskManager.PushBack(ts, delegate
        {
            _sound.fall.Play();
            refreshModel2View();
        });
        return ts;
    }
   private  State fallState()
    {
        StateWithEventMap state = TaskState.Create(delegate
        {
            Task fall = doFall();
            //TaskWait tw = new TaskWait();
            //tw.setAllTime(0.5f);
            //TaskManager.PushBack(tw, delegate
            //{
            //    doFall();
            //});
            return fall;
        }, fsm_, "remove");
        state.onStart += delegate
        {
            //Debug.LogWarning("in fall!");
        };
        return state;
    }
bool checkRemove()
    {
        List<Cube> removeCube = new List<Cube>();
        bool cr = false;
        for (int x = 0; x < _model.width; ++x)
        {
            for (int y = 0; y < _model.height; ++y)
            {
                Cube c = _model.getCube(x, y);
                if (c.isEnable == true)
                {
                    Cube up = _model.getCube(x, y - 1);
                    Cube down = _model.getCube(x, y + 1);
                    Cube left = _model.getCube(x - 1, y);
                    Cube right = _model.getCube(x + 1, y);
                    if (up != null && up.isEnable == true && up.number + c.number == 10)
                    {
                        removeCube.Add(c);
                    }                   
                   else if (down != null && down.isEnable == true && down.number + c.number == 10)
                    {
                        removeCube.Add(c);
                    }
                   else if (left != null && left.isEnable == true && left.number + c.number == 10)
                    {
                        removeCube.Add(c);                
                    }
                   else if (right != null && right.isEnable == true && right.number + c.number == 10)
                    {
                        removeCube.Add(c);
                    }
                }
            }
        }
        foreach(Cube cu in removeCube)
        {           
            cu.isEnable = false;
            cr = true;
            score += removeCube.Count;
            _sound.remove.Play();
            //_view._score = score;
            //_view._endScore = score;
        }        
        refreshModel2View();
        return cr;
    }
    State removeState()
    {
        bool cr = false;
        StateWithEventMap state = TaskState.Create(delegate
        {
            Task task = new Task();
            //增加一个操作
            TaskManager.PushFront(task, delegate
            {
                cr = checkRemove();
            });
            return task;
        }, fsm_, delegate
        {
            if (cr)
            {
                return "fall";
            }
            else
            {
                return "input";
            }
        });
        state.onStart += delegate
        {
            //Debug.LogWarning("in remove!");
        };
        return state;
    }
    void Start()
    {
        fsm_.addState("begin", beginState());
        fsm_.addState("play", playState());

        fsm_.addState("input", inputState(), "play");
        fsm_.addState("fall", fallState(), "play");
        fsm_.addState("remove", removeState(), "play");

        fsm_.addState("end", endState());
        fsm_.init("begin");
    }

    // Update is called once per frame
    void Update()
    {
        _view._score = score;
        _view._endScore = score;
        over(0);
        over(1);
        over(2);
        over(3);
    }
    public void over(int x)
    {
        int unEnableCube = 0;
        for (int y = _model.height - 1; y >= 0; --y)
        {
            Cube c = _model.getCube(x, y);
            if (c.isEnable == false)
            {
                unEnableCube += 1;
            }
        }
        if (unEnableCube == 0)
        {
            fsmPost("GameOver");
        }
    }
}
