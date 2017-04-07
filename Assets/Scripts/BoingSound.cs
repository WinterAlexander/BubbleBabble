using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoingSound : MonoBehaviour {
    private AudioSource source;
    private AudioClip[] clips;
	// Use this for initialization
	void Start () {
        clips = GameObject.Find("AudioMaster").GetComponent<AudioContainer>().audios;
        source = GetComponent<AudioSource>();
        source.volume = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision other)
    {
        source.volume = 1f;
        source.clip = clips[Random.Range(0, clips.Length - 1)];
        source.pitch = UnityEngine.Random.Range(0.8f, 1f);
        source.Play();
    }
}
