using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoingSound : MonoBehaviour {
    private AudioSource source;
	// Use this for initialization
	void Start () {
        source = gameObject.GetComponent<AudioSource>();
        source.volume = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision other)
    {
        source.volume = 0.25f;
        source.pitch = UnityEngine.Random.Range(0.8f, 1f);
        source.Play();
    }
}
