using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewPlayerScreen : MonoBehaviour
{
    [Header("Teams")]
    public int selectedTeam;
    public Team team1;
    public Team team2;

    [Header("Roles")]
    public int selectedRole;
    public GameObject riderPrefab;
    public GameObject runnerPrefab;
    public InputActionAsset riderControls;
    public InputActionAsset runnerControls;

    [Header("Buttons")]
    public Button team1Button;
    public Button team2Button;
    public Button riderButton;
    public Button runnerButton;
    public Button doneButton;

    void Update()
    {
        doneButton.interactable = (selectedTeam == 1 || selectedTeam == 2) && (selectedRole == 1 || selectedRole == 2);
    }

    public void ChooseTeam(int teamID)
    {
        if (teamID == selectedTeam)
        {
            selectedTeam = 0;
            team1Button.interactable = true;
            team2Button.interactable = true;
        }
        else if (teamID == 1)
        {
            selectedTeam = 1;
            team2Button.interactable = false;
        }
        else if (teamID == 2)
        {
            selectedTeam = 2;
            team1Button.interactable = false;
        }
    }

    public void ChooseRole(int roleID)
    {
        if (roleID == selectedRole)
        {
            selectedRole = 0;
            riderButton.interactable = true;
            runnerButton.interactable = true;
        }
        else if (roleID == 1)
        {
            selectedRole = 1;
            runnerButton.interactable = false;
        }
        else if (roleID == 2)
        {
            selectedRole = 2;
            riderButton.interactable = false;
        }
    }

    public void Done()
    {
        GameObject prefab = selectedRole == 1 ? riderPrefab : runnerPrefab;
        try {
            Gamepad gamepad = Gamepad.all[GameManager.playerCount];
            PlayerInput player = PlayerInput.Instantiate(prefab, GameManager.playerCount, "", selectedTeam-1, gamepad);
            CameraFollow cam = Instantiate(GameManager.Instance.cameraPrefab).GetComponent<CameraFollow>();
            player.camera = cam.gameObject.GetComponent<Camera>();
            cam.target = player.transform;
            GameManager.Instance.SetTeam(player, selectedTeam, selectedRole);

            GameManager.playerCount++;
            gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            Console.Error(e);
        }
    }
}
