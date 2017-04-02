using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    public static void FreezeFrame(float sec)
    {
        if (Camera.main.gameObject.GetComponent<FreezeComponent>() == null)
            Camera.main.gameObject.AddComponent<FreezeComponent>().freezeSec = sec;
    }

    public static void FreezeFrame()
    {
        if (Camera.main.gameObject.GetComponent<FreezeComponent>() == null)
            Camera.main.gameObject.AddComponent<FreezeComponent>().freezeSec = .1f;
    }

    public class FreezeComponent : MonoBehaviour
    {
        public OnFreezeEndAction action;
        public float freezeSec;

        void Start()
        {
            StartCoroutine(FreezeFrameEffect());
        }

        IEnumerator FreezeFrameEffect()
        {
            Time.timeScale = 0.01f;
            float pauseEndTime = Time.realtimeSinceStartup + freezeSec;
            while (Time.realtimeSinceStartup < pauseEndTime)
                yield return 0;

            Time.timeScale = 1;

            if (action != null)
                action.Act();

            Destroy(this);
        }
    }
}
