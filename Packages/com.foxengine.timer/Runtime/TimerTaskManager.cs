using System;
using System.Collections;
using System.Collections.Generic;
using FoxEngine;
using UnityEngine;
using UnityEngine.Events;

public class TimerTaskManager : Singleton<TimerTaskManager>
{
    private FoxDictionary<TimerTask, Coroutine> timersTasks = new FoxDictionary<TimerTask, Coroutine>();

    [Header("ReadOnly")] 
    [SerializeField] private List<TimerTask> timerList = new List<TimerTask>();

    public void StartTimer(TimerTask _timerTask)
    {
        timersTasks.Add(_timerTask, StartCoroutine(StartTimerCoroutine(_timerTask)));
        timerList.Add(_timerTask);
        _timerTask.OnStart();
    }

    public bool IsStarted(TimerTask _timerTask)
    {
        return timersTasks.ContainsKey(_timerTask);
    }

    private IEnumerator StartTimerCoroutine(TimerTask _timerTask)
    {
        while(_timerTask.Timer < _timerTask.Time)
        {
            if (!_timerTask.IsPausing)
                _timerTask.Timer += Time.deltaTime;

            yield return null;
        }
        
        Finish(_timerTask);
    }

    private  void Finish(TimerTask _timerTask)
    {
         UnityAction finish = _timerTask.OnTimerFinish;
        timersTasks.Remove(_timerTask);
        timerList.Remove(_timerTask);
        finish?.Invoke();
        _timerTask.OnFinish(); 
    }

    public void Cancel(TimerTask _timerTask)
    {
        StopCoroutine(timersTasks[_timerTask]);
        timersTasks.Remove(_timerTask);
        timerList.Remove(_timerTask);
    }
}

[Serializable]
public class TimerTask
{
    [SerializeField] protected string name;
    [SerializeField] protected float time;
    [SerializeField] protected UnityAction onTimerFinish;
    [SerializeField] protected bool isPausing;
    [SerializeField] protected float timer;

    public float Time => time;
    public float Timer
    {
        get => timer;
        set => timer = value;
    }

    public bool IsPausing => isPausing;
    public UnityAction OnTimerFinish => onTimerFinish;

    public TimerTaskManager Manager => TimerTaskManager.Instance;

    public TimerTask(string _name, float _time,UnityAction _callback)
    {
        name = _name;
        time = _time;
        timer = 0;
        onTimerFinish = _callback;
    }

    public virtual void Start()
    {
        if (!Manager.IsStarted(this))
        {
            timer = 0;
            Manager.StartTimer(this);
        }
        else
            FoxEngine.Debug.LogError($"[TimerTask] This timer is already running ! ({timer}/{time})");
    }

    public virtual void Resume()
    {
        if (!Manager.IsStarted(this))
            FoxEngine.Debug.LogError($"[TimerTask] This timer isn't running !");
        else
            isPausing = false;
    }

    public virtual void OnStart()
    { 
    
    }

    public virtual void OnFinish()
    {
        
    }

    public virtual bool InProgress()
    {
        return TimerTaskManager.Instance.IsStarted(this);
    }
    
    public virtual void Pause(bool _log = true)
    {
        if (!Manager.IsStarted(this))
        {
            if(_log)
                FoxEngine.Debug.LogError($"[TimerTask] This timer isn't running !");
        }
        else
            isPausing = true;
    }

    public static TimerTask Copy(TimerTask _task)
    {
        return new TimerTask(_task.name,_task.time,_task.onTimerFinish);
    }

    public virtual TimerTask Copy()
    {
        return new TimerTask(name,time,onTimerFinish);
    }

    public virtual void Cancel(bool _log = true)
    {
        if (!Manager.IsStarted(this))
        {
            if(_log)
                FoxEngine.Debug.LogError($"[TimerTask] This timer isn't running !");
        }
        else
        {
            Manager.Cancel(this);
            timer = 0;
        }
    }

    public virtual void OnDestroy()
    {
        Cancel();
    }
}
