﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Rider : MonoBehaviour
{
    #region VARIABLES

        public int teamID;
        public Team teamPlayer;
        public Rigidbody rb;
        public bool enableCollisions;

        #region ABILITIES
            [Header("Abilities")]
            [Command("kb_str")]
            public float knockbackStrength;
            public UnityEvent Knockback;
        #endregion

        #region CONTROLS
            [Header("Controls")]
            public PlayerInput input;
            public float aimAngle;
         
            public bool queueKnockback;
        #endregion

    #endregion

    // void Awake()
    // {
        
    // }
    
    void OnEnable() { Parser.Register(this, "buddy" + teamID); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        teamPlayer = GetComponent<Team>();
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = enableCollisions;
        input = GetComponent<PlayerInput>();
    }

    // Input Actions
    public void OnAim(InputValue value) { aimAngle = Mathf.Atan2(value.Get<Vector2>().y, value.Get<Vector2>().x) * Mathf.Rad2Deg; }
    public void OnKnockback() { Knockback.Invoke(); }

    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(aimAngle - 90, Vector3.up);
    }

    void FixedUpdate()
    {
        
    }

    public void Fall()
    {
        rb.detectCollisions = true;
    }

    public void PickedUp()
    {
        rb.detectCollisions = false;
    }
}