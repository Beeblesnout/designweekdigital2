using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;

public class GameManager : SingletonBase<GameManager>
{


    // public override void Awake()
    // {
    //     base.Awake();
        
    // }

    void OnEnable() { Parser.Register(this, "gm"); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        Console.Open = false;
    }
}
