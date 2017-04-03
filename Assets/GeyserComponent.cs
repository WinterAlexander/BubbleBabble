using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserComponent : MonoBehaviour {

    private GameObject emitter;
    public float deadTime = 6f;
    public float secondsToWait = 4f;
    private float startHeight;

    List<Transform> beingGeysed = new List<Transform>();

    void Start () {
        emitter = gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
        startHeight = emitter.transform.position.y;
        StartCoroutine(startGeysing());
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    IEnumerator startGeysing()
    {
        ArrayList players = GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>().GetPlayers();
        
        foreach(GameObject p in players)
        {
            Debug.Log(p.name);
            float dis = MathUtils.XZDist(p.transform.position, gameObject.transform.position);

            if(dis <= 1 && !beingGeysed.Contains(p.transform))
            {
                beingGeysed.Add(p.transform);
                StartCoroutine(raise(p.transform));
            }
        }

        emitter.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(secondsToWait);
        StartCoroutine(wait());
    }

    IEnumerator raise(Transform transform)
    {
        for(int i = 0; i < 240; i++)
        {
            transform.position = new Vector3(emitter.transform.position.x,
                                            startHeight + i / 50.0f,
                                            emitter.transform.position.z);
            yield return null;
        }       

        beingGeysed.Remove(transform);
    }

    IEnumerator wait()
    {
        emitter.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(deadTime);
        StartCoroutine(startGeysing());
    }
}
