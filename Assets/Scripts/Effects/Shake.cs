using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    public static void ShakeEffect(GameObject obj)
    {
        ShakeEffect(obj, 1, .2f);
    }

    public static void ShakeEffect()
    {
        ShakeEffect(Camera.main.gameObject, 1, .2f);
    }

    public static void ShakeEffect(GameObject obj, float intensity)
    {
        ShakeEffect(obj, intensity, .2f);

    }
  
    public static void ShakeEffect(GameObject obj, float intensity, float time)
    {
        if (obj.GetComponent<ShakeComponent>() == null)
            obj.AddComponent<ShakeComponent>();
        ShakeComponent shake = obj.GetComponent<ShakeComponent>();
        shake.shake = time;
        shake.shakeAmount = intensity;
    }

    private class ShakeComponent : MonoBehaviour
    {

        public Transform shakeTransform;

        public float shake = 0.1f;

        public float shakeAmount = 0.10f;
        public float decreaseFactor = 0.7f;

        Vector3 originalPos;


        void Awake()
        {
            if (shakeTransform == null)
            {
                shakeTransform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            originalPos = shakeTransform.localPosition;
        }

        void Update()
        {
            if (shake > 0)
            {
                shakeTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shake -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shake = 0f;
                shakeTransform.localPosition = originalPos;
                Destroy(this);
            }
        }
    }
}
