using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserComponent : MonoBehaviour {

    private GameObject emitter;
    private GameObject preEmitter;
    private float deadTime = 8f;
    private float secondsActive = 6f;
    private float ceilingHeight;
    private float startHeight;
    private bool isGeysing;

    void Start () {
        emitter = gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
        preEmitter = gameObject.GetComponentsInChildren<Transform>()[2].gameObject;
        startHeight = emitter.transform.position.y;
        ceilingHeight = startHeight + 7f;
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

                if (dis <= 0.75 && p.transform.position.y >= startHeight - 0.75f && p.transform.position.y <= ceilingHeight && p.GetComponent<Rigidbody>().velocity.y < 5)
                {
                    p.GetComponent<Rigidbody>().velocity = new Vector3(0, 8, 0);
                }
            }
        }        
    }

    IEnumerator StartGeysing()
    {
        isGeysing = true;
        preEmitter.GetComponent<ParticleSystem>().Stop();
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
        preEmitter.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(deadTime);
        StartCoroutine(StartGeysing());
    }
}
