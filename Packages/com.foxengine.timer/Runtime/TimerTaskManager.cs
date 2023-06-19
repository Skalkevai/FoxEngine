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
    [SerializeField] private string name;
    [SerializeField] private float time;
    [SerializeField] private UnityAction onTimerFinish;
    [SerializeField] private bool isPausing;
    [SerializeField] private float timer;

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

    public void Start()
    {
        if (!Manager.IsStarted(this))
        {
            timer = 0;
            Manager.StartTimer(this);
        }
        else
            FoxEngine.Debug.DebugError($"[TimerTask] This timer is already running ! ({timer}/{time})");
    }

    public void Resume()
    {
        if (!Manager.IsStarted(this))
            FoxEngine.Debug.DebugError($"[TimerTask] This timer isn't running !");
        else
            isPausing = false;
    }

    public bool InProgress()
    {
        return TimerTaskManager.Instance.IsStarted(this);
    }
    
    public void Pause()
    {
        if (!Manager.IsStarted(this))
            FoxEngine.Debug.DebugError($"[TimerTask] This timer isn't running !");
        else
            isPausing = true;
    }
    
    public void Cancel()
    {
        if (!Manager.IsStarted(this))
            FoxEngine.Debug.DebugError($"[TimerTask] This timer isn't running !");
        else
        {
            Manager.Cancel(this);
            timer = 0;
        }
    }

    public void OnDestroy()
    {
        Cancel();
    }
}
