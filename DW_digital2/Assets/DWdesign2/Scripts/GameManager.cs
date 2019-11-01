using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Linq;
using UnityEngine.Events;

public class GameManager : SingletonBase<GameManager>
{
    public static int playerCount;
    public Team team1;
    public Team team2;

    public GameObject cameraPrefab;
    public Transform winScreen;

    public UnityEvent OnWin;

    void OnEnable() { Parser.Register(this, "gm"); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        Console.Open = false;
    }
    
    public void SetTeam(PlayerInput player, int teamID, int roleID)
    {
        Team team = teamID == 1 ? team1 : team2;
        if (roleID == 1) team.rider = player.gameObject;
        else team.runner = player.gameObject;
        player.camera = team.cameraRig.camera;
    }

    [Command("showdevices")]
    public static void ShowDevices()
    {
        string message = "Keyboards:\n";
        for (int i = 0; i < Keyboard.all.Count; i++)
        {
            message += "\t" + i + ": " + Keyboard.all[i].displayName + "\n";
        }

        message += "Gamepads:\n";
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            message += "\t" + i + ": " + Gamepad.all[i].displayName + "\n";
        }
        Console.print(message);
    }

    public void Win(int team)
    {
        winScreen.gameObject.SetActive(true);
    }

    [Command("Quit")]
    public static void Quit()
    {

    }
}
