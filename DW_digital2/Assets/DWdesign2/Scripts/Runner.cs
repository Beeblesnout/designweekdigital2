using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Popcron.Console;
using Beeble;
using UnityEngine.InputSystem;

public class Runner : MonoBehaviour
{
    #region VARIABLES

        #region TEAM
            public int teamID;
            public Team team;
            public Transform riderLoc;
        #endregion

        #region MOVEMENT
            // Movement
            [Header("Movement")]
            float maxSpeed;
            [Command("maxspeed")]
            public float defaultMaxSpeed;
            float accel;
            [Command("accel")]
            public float defaultAccel;
            [Command("maxdeccel")]
            public float maxDeccelRate;
            
            // Jump
            public bool grounded;
            [Command("jumpstrength")]
            public float jumpStrength;
            Timer jumpCDTimer;
            [Command("jumpcd")]
            public float jumpCDDuration;

            // Dash
            public bool dashing;
            [Command("dashspeedmod")]
            public float dashSpeedMod;
            [Command("dashaccelmod")]
            public float dashAccelMod;
            Timer dashTimer;
            [Command("dashduration")]
            public float dashDuration;
            Timer dashCDTimer;
            [Command("dashcd")]
            public float dashCDDuration;

            // Component References
            Rigidbody rb;
            public Collider shieldCollider;
        #endregion

        #region CONTROLS
            [Header("Controls")]
            public int cIndex;
            public bool usingKeyboard;
            public Vector3 inputVect;

            // Jump
            public bool canJump;
            public bool queueJump;
            public UnityEvent OnJumpStart;

            // Dash
            public bool canDash;
            public bool queueDash;
            public UnityEvent OnDashStart;
            public UnityEvent OnDashEnd;
        #endregion

    #endregion

    // void Awake()
    // {
        
    // }
    
    void OnEnable() { Parser.Register(this, "runner" + teamID); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        accel = defaultAccel;
        maxSpeed = defaultMaxSpeed;

        jumpCDTimer = new Timer(jumpCDDuration);
        dashTimer = new Timer(dashDuration);
        dashCDTimer = new Timer(dashCDDuration);
        dashTimer.Start();
    }

    // Input Actions
    // public void OnMove(InputValue value) { inputVect = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y); }
    // public void OnJump() { queueJump ^= grounded; }
    // public void OnDash() { queueDash ^= !dashing; }

    void Update()
    {
        // Input
        var i = Gamepad.all[cIndex].leftStick.ReadValue();
        inputVect = new Vector3(i.x, 0, i.y);

        bool jmp = false;
        if (Gamepad.all[cIndex].buttonSouth.ReadValue() > 0) jmp = true;
        if (usingKeyboard && Keyboard.current.spaceKey.ReadValue() > 0) jmp = true;
        if (jmp && grounded && jumpCDTimer.Completed)
        {
            queueJump ^= true;
        }

        bool dsh = false;
        if (Gamepad.all[cIndex].leftShoulder.ReadValue() > 0) dsh = true;
        if (usingKeyboard && Keyboard.current.leftShiftKey.ReadValue() > 0) dsh = true;
        if (dsh && !dashing && dashCDTimer.Completed)
        {
            queueDash ^= true;
        }

        jumpCDTimer.duration = jumpCDDuration;
        dashTimer.duration = dashDuration;
        dashCDTimer.duration = dashCDDuration;

        jumpCDTimer.Tick();
        dashCDTimer.Tick();
        if (dashing) dashTimer.Tick();
    }

    void FixedUpdate()
    {
        // Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, .2f, LayerMask.GetMask("Ground"));

        // Jump
        if (queueJump)
        {
            queueJump = false;
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jumpCDTimer.Start();
            OnJumpStart.Invoke();
        }

        // Dash
        if (queueDash)
        {
            queueDash = false;
            maxSpeed += dashSpeedMod;
            accel += dashAccelMod;
            dashing = true;
            dashTimer.Start();
            OnDashStart.Invoke();
        }
        if (dashing)
        {
            maxSpeed = defaultMaxSpeed + dashSpeedMod;
            accel = defaultAccel + dashAccelMod;

            if (dashTimer.Completed)
            {
                dashing = false;
                dashCDTimer.Start();
                OnDashEnd.Invoke();
            }
        }
        else
        {
            maxSpeed = defaultMaxSpeed;
            accel = defaultAccel;
        }

        // Movement
        float overspeed = Mathf.Max(0, rb.velocity.magnitude - maxSpeed);
        if (inputVect.magnitude > .1f)
        {
            rb.velocity += inputVect * accel;

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.LerpUnclamped(rb.velocity.normalized * maxSpeed, rb.velocity, 1-maxDeccelRate);
            }
        }
        else
        {
            rb.velocity = Vector3.LerpUnclamped(new Vector3(0, rb.velocity.y, 0), rb.velocity, 1-maxDeccelRate*2);
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Rider rider = other.gameObject.GetComponent<Rider>();
            if (rider && rider.teamID == teamID)
            {
                rider.PickedUp();
            }
        }
        else if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
