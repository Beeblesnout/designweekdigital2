using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public new Camera camera;

    public Transform target;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Start() {
        
    }

    void Update () {
        //transform.parent.position = target.position + Vector3.up;
    }

}
