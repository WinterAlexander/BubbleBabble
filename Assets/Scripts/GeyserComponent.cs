using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserComponent : MonoBehaviour {

    private GameObject emitter;
    public float deadTime = 8f;
    public float secondsActive = 6f;
    private float startHeight;
    private bool isGeysing;

    void Start () {
        emitter = gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
        startHeight = emitter.transform.position.y;
        isGeysing = false;
        StartCoroutine(StartGeysing());      
    }
	
	// Update is called once per frame
	void Update () {

        if(isGeysing)
        {
            ArrayList players = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>().GetPlayers();

            foreach (GameObject p in players)
            {
                float dis = MathUtils.XZDist(p.transform.position, gameObject.transform.position);

                if (dis <= 1)
                {
                    p.GetComponent<Rigidbody>().AddForce(new Vector3(0, 15f, 0), ForceMode.Force);
                }
            }
        }        
    }

    IEnumerator StartGeysing()
    {
        isGeysing = true;
        emitter.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(secondsActive);
        StartCoroutine(Wait());
    }

    //IEnumerator Raise(Transform transform)
    //{
    //    for(int i = 0; i < 240 && isGeysing; i++)
    //    {
    //        transform.position = new Vector3(emitter.transform.position.x,
    //                                        startHeight + i / 50.0f,
    //                                        emitter.transform.position.z);
    //        yield return null;
    //    }
    //    transform.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    beingGeysed.Remove(transform);
    //}

    IEnumerator Wait()
    {
        isGeysing = false;
        emitter.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(deadTime);
        StartCoroutine(StartGeysing());
    }
}
