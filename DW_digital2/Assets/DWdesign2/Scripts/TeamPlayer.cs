using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;

public class TeamPlayer : MonoBehaviour
{
    public int teamID;
    Buddy buddy;
    Runner runner;

    void Awake()
    {
        buddy = GetComponent<Buddy>();
        runner = GetComponent<Runner>();
        buddy.teamID = teamID;
        runner.teamID = teamID;
    }
    
    void OnEnable() { Parser.Register(this, "team" + teamID); }
    void OnDisable() { Parser.Unregister(this); }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
