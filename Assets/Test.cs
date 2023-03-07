//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Test : MonoBehaviour
//{
//    private TimerTask tt;
    
//    // Start is called before the first frame update
//    void Start()
//    {
//        tt = new TimerTask(2f, () => {Debug.Log("TimerTask is finish"); });
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            tt.Start();
//            UnityEngine.Debug.Log("TimerTask is starting !!");
//        }

//        if (Input.GetKeyDown(KeyCode.P))
//        {
//            tt.Pause();
//            UnityEngine.Debug.Log("TimerTask is pausing !!");
//        }

//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            tt.Resume();
//            UnityEngine.Debug.Log("TimerTask is resuming !!");
//        }

//        if (Input.GetKeyDown(KeyCode.C))
//        {
//            tt.Cancel();
//            UnityEngine.Debug.Log("TimerTask is cancel !!");
//        }
//    }
//}
