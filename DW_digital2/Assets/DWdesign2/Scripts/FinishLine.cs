using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    public UnityEvent<GameObject> OnFinish;
    public Transform spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = spawn.position;
            OnFinish.Invoke(other.gameObject);
        }
    }
}
