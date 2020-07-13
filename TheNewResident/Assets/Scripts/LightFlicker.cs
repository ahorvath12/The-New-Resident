using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    float timeOn = 0.1f;
    float timeOff = 0.5f;

    private float changeTime;
    private Light lightCom;

    private void Start()
    {
        changeTime = 0f;
        lightCom = gameObject.GetComponent<Light>();
    }

    void Update()
    {
        timeOn = Random.Range(0.1f, 0.3f);
        timeOff = Random.Range(0.1f, 0.3f);

        if (Time.time > changeTime)
        {
            lightCom.enabled = !lightCom.enabled;
            if (lightCom.enabled)
            {
                changeTime = Time.time + timeOn;
            }
            else
            {
                changeTime = Time.time + timeOff;
            }
        }
    }
}
