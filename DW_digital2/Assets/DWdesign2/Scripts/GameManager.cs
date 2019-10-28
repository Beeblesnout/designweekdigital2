using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        Console.Initialize();
        Console.Open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
