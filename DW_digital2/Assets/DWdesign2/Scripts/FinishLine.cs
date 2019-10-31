using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    public GameObject player;
    public Transform spawn;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = spawn.position;
    }
}
