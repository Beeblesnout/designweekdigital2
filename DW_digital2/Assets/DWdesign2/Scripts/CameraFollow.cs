using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public new Camera camera;
    [Range(0, 1)]
    public float blendPos;
    public Transform target1;
    public Transform target2;
    public float maxMoveSpeed;

    void Update () {
        Vector3 destination = Vector3.Lerp(target1.position, target2.position, .75f);
        transform.position = Vector3.Lerp(transform.position, destination, .1f);
    }

}
