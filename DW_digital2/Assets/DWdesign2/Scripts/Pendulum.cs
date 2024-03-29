﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float angle = 90.0f;
    public float speed = 1.5f;
    public bool activateR;
    public bool activateL;
    Quaternion qStart, qEnd;
    private float startTime;
    public Vector3 axis;

    void Start()
    {
        qStart = Quaternion.AngleAxis(angle, axis.normalized);
        qEnd = Quaternion.AngleAxis(-angle, axis.normalized);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Mathf.PingPong(Time.time * speed, length), transform.position.y, transform.position.z);
        if (activateR == true)
        {
            startTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(qStart, qEnd, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1.0f) / 2.0f);
        }
        if (activateR == false && activateL == false)
        {
            resetTimer();
        }

    }

    void resetTimer()
    {
        startTime = 0.0f;
    }
}
