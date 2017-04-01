using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour {

	void Start () {
		
	}

    void Update()
    {
        gameObject.transform.Rotate(1, 1, 0);

        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            Mathf.Sin(Time.time)/2f + 1.5f,
            gameObject.transform.position.z);
    }
}




















