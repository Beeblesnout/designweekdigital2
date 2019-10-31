using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;
using UnityEngine.InputSystem;

public class GameManager : SingletonBase<GameManager>
{
    public static int playerCount;
    public Team team1;
    public Team team2;

    public GameObject newPlayerScreen;
    public GameObject cameraPrefab;

    void OnEnable() { Parser.Register(this, "gm"); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        Console.Open = false;
    } 

    [Command("addplayer")]
    public void AddPlayer()
    {
        if (playerCount == 4)
        {
            Console.Warn("Player count at maximum (4).");
            return;
        }
        else
        {
            newPlayerScreen.SetActive(true);
        }
    }
    
    public void SetTeam(PlayerInput player, int teamID, int roleID)
    {
        Team team = teamID == 1 ? team1 : team2;
        if (roleID == 1) team.rider = player.gameObject;
        else team.runner = player.gameObject;
        player.camera = team.cameraRig.camera;
    }
}
