using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    public UnityEvent OnFinish;
    public Transform spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = spawn.position;
            GameManager.Instance.Win(other.name.Contains("Team1") ? 1 : 2);
        }
    }
}
