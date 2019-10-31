using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;
using UnityEngine.InputSystem;

public class Team : MonoBehaviour
{
    public int teamID;
    public GameObject rider;
    public GameObject runner;
    public CameraFollow cameraRig;
    
    void OnEnable() { Parser.Register(this, "team" + teamID); }
    void OnDisable() { Parser.Unregister(this); }
    
}
