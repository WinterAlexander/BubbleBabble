using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAnimation : MonoBehaviour {

    public float rotationSpeed = 0.04f;
    public float scaleCenter = 0.8f;
    public float scaleDispersion = 0.2f;
	// Use this for initialization
	void Start () {
        gameObject.transform.Rotate(0, 0, -rotationSpeed * 60);
    }
	
	// Update is called once per frame
	void Update () {
             gameObject.transform.Rotate(0, 0, Mathf.Sin(Time.time) * rotationSpeed);
        gameObject.transform.localScale = new Vector3(Mathf.Abs(Mathf.Sin(Time.time)) * scaleDispersion + scaleCenter,
                                                      Mathf.Abs(Mathf.Cos(Time.time)) * scaleDispersion + scaleCenter);


    }
}
