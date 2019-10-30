using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Popcron.Console;
using Beeble;

public class Runner : MonoBehaviour
{
    #region VARIABLES

        #region TEAM
            public int teamID;
            public TeamPlayer teamPlayer;
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
        #endregion

        #region CONTROLS
            [Header("Controls")]
            /// <summary>
            /// The index of this player's controller.
            /// </summary>
            public int cIndex;
            public Vector3 input;

            // Jump
            bool queueJump;
            public UnityEvent OnJump;

            // Dash
            bool queueDash;
            public UnityEvent OnDashStart;
            public UnityEvent OnDashEnd;
        #endregion

    #endregion

    void Awake()
    {
        
    }
    
    void OnEnable() { Parser.Register(this, "runner" + teamID); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        teamPlayer = GetComponent<TeamPlayer>();
        rb = GetComponent<Rigidbody>();

        accel = defaultAccel;
        maxSpeed = defaultMaxSpeed;

        jumpCDTimer = new Timer(jumpCDDuration);
        dashTimer = new Timer(dashDuration);
        dashCDTimer = new Timer(dashCDDuration);
    }

    void Update()
    {
        // idk why i have to flip vertical input
        input = new Vector3(Input.GetAxis("Horizontal" + cIndex), 0, -Input.GetAxis("Vertical" + cIndex));

        // TODO: these dont work at all
        if (grounded) queueJump ^= Input.GetButtonDown("ActionA" + cIndex);
        if (!dashing && !dashCDTimer.Completed) queueDash ^= Input.GetButtonDown("ActionB" + cIndex);

        jumpCDTimer.duration = jumpCDDuration;
        dashTimer.duration = dashDuration;
        dashCDTimer.duration = dashCDDuration;
    }

    void FixedUpdate()
    {
        // Ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, .1f, LayerMask.GetMask("Ground"));

        // Jump
        if (queueJump)
        {
            queueJump = false;
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            OnJump.Invoke();
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

            dashTimer.Tick();
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

            dashCDTimer.Tick();
        }

        // Movement
        float overspeed = Mathf.Max(0, rb.velocity.magnitude - maxSpeed);
        if (input.magnitude > .1f)
        {
            // rb.AddForce(input * accel, ForceMode.Acceleration);
            rb.velocity += input * accel;
            // print(rb.velocity);

            if (rb.velocity.magnitude > maxSpeed)
            {
                // rb.velocity = rb.velocity.normalized * Mathf.Min(overspeed, maxDeccelRate);
                rb.velocity = Vector3.LerpUnclamped(rb.velocity.normalized * maxSpeed, rb.velocity, 1-maxDeccelRate);
            }
        }
        else
        {
            // rb.velocity = rb.velocity.normalized * Mathf.Min(Mathf.Pow(overspeed, 2), maxDeccelRate * 2);
            rb.velocity = Vector3.LerpUnclamped(new Vector3(0, rb.velocity.y, 0), rb.velocity, 1-maxDeccelRate*2);
        }
    }
}
