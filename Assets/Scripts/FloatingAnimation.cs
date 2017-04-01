using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour {
    public float startY;

	void Start () {
        if (startY == null)
            startY = gameObject.transform.position.y;
	}

    void Update()
    {
        gameObject.transform.Rotate(1, 1, 0);

        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            Mathf.Sin(Time.time)/2f + startY,
            gameObject.transform.position.z);
    }
}




















