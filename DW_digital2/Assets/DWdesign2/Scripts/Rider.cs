using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Timer = Beeble.Timer;

public class Rider : MonoBehaviour
{
    #region VARIABLES

        public int teamID;
        public Team team;
        public Rigidbody rb;
        public bool enableCollisions;
        public Transform knockbackPivot;
        public Transform shieldPivot;
        public float shieldRotateSpeed;
        float timeCount = 0;
        public Transform shield;

        #region ABILITIES
            [Header("Abilities")]
            [Command("kb_str")]
            public float knockbackStrength;
            Timer knockbackCD;
            public float knockbackCDDur;
            public UnityEvent DoKnockback;
        #endregion

        #region CONTROLS
            [Header("Controls")]
            public int cIndex;
            public bool usingKeyboard;
            public float aimAngle;
            float smoothAimAngle;
        
            public bool queueKnockback;
        #endregion

    #endregion
    
    void OnEnable() { Parser.Register(this, "rider" + teamID); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        team = GetComponent<Team>();
        rb = GetComponent<Rigidbody>();
        rb.detectCollisions = enableCollisions;

        knockbackCD = new Timer();
    }

    // Input Actions
    // public void OnAim(InputValue value) { aimAngle = Mathf.Atan2(value.Get<Vector2>().y, value.Get<Vector2>().x) * Mathf.Rad2Deg; }
    // public void OnKnockback() { Knockback.Invoke(); }

    void Update()
    {
        var input = Gamepad.all[cIndex].leftStick.ReadValue();
        aimAngle = Mathf.Atan2(input.y, -input.x) * Mathf.Rad2Deg;

        bool kB = false;
        if (Gamepad.all[cIndex].buttonSouth.ReadValue() > 0) kB = true;
        if (usingKeyboard && Keyboard.current.kKey.ReadValue() > 0) kB = true;
        if (kB && knockbackCD.Completed) 
        {
            knockbackCD.Start();
            DoKnockback.Invoke();
        }

        // Rotation
        knockbackPivot.rotation = Quaternion.AngleAxis(aimAngle - 90, Vector3.up);
        var deltaAngle = aimAngle - smoothAimAngle;
        smoothAimAngle += Mathf.Min(shieldRotateSpeed, Mathf.Abs(deltaAngle)) * Mathf.Sign(deltaAngle);
        shieldPivot.rotation = Quaternion.AngleAxis(smoothAimAngle - 90, Vector3.up);
        timeCount += Time.deltaTime * shieldRotateSpeed;

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
