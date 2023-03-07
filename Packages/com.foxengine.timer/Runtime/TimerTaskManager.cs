using System.Collections;

public class TimerTaskManager : Singleton<TimerTaskManager>
{
    private TimerTask timerTask;
    private bool isPausing;
    
    public void SetTimer(TimerTask _timerTask)
    {
        timerTask = _timerTask;
    }

    public void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        float timer = 0;
        while(timer < timerTask.Time)
        {
            if(!isPausing)
                timer += Time.deltaTime;
            yield return null;
        }
        
        Finish();
    }

    private  void Finish()
    {
        timerTask.OnTimerFinish?.Invoke();
        Destroy(gameObject);
    }

    public void Pause()
    {
        isPausing = true;
    }

    public void Resume()
    {
        isPausing = false;
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}

public class TimerTask
{
    private float timer;
    private UnityAction onTimerFinish;
    private TimerTaskObject timerTaskObject;

    public float Time => timer;
    public UnityAction OnTimerFinish => onTimerFinish;
        
    public TimerTask(float _timer,UnityAction _callback)
    {
        timer = _timer;
        onTimerFinish = _callback;
    }

    public void Start()
    {
        if (timerTaskObject != null)
        {
            UnityEngine.Debug.LogError("[TimerTask] Couldn't start again, the timerTask already running !!");
            return;
        }
        
        timerTaskObject = new GameObject("TimerTask").AddComponent<TimerTaskObject>();
        timerTaskObject.SetTimer(this);
    }

    public void Resume()
    {
        if(timerTaskObject == null)
            UnityEngine.Debug.LogError("[TimerTask] Couldn't resume, the timerTask isn't started !!");
        else
            timerTaskObject.Resume();

        timerTaskObject.Resume();
    }

    public void Pause()
    {
        if(timerTaskObject == null)
            UnityEngine.Debug.LogError("[TimerTask] Couldn't pause, the timerTask isn't started !!");
        else
            timerTaskObject.Pause();
    }
    
    public void Finish()
    {
        timerTaskObject = null;
    }
    
    public void Cancel()
    {
        if(timerTaskObject == null)
            UnityEngine.Debug.LogError("[TimerTask] Couldn't cancel, the timerTask isn't started !!");
        else
        {
            timerTaskObject.Cancel();
            timerTaskObject = null; 
        }
    }
}