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

    public float timeCount;

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
            float prevAngle;
            float smoothAimAngle;
        
            public bool queueKnockback;
        #endregion

    #endregion
    
    void OnEnable() { Parser.Register(this, "rider" + teamID); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.detectCollisions = enableCollisions;

        knockbackCD = new Timer(knockbackCDDur);
    }

    // Input Actions
    // public void OnAim(InputValue value) { aimAngle = Mathf.Atan2(value.Get<Vector2>().y, value.Get<Vector2>().x) * Mathf.Rad2Deg; }
    // public void OnKnockback() { Knockback.Invoke(); }

    void Update()
    {
        // var input = Gamepad.all[cIndex].leftStick.ReadValue();
        var input = Gamepad.all[cIndex].rightStick.ReadValue();
        if (input.magnitude > .1) aimAngle = Mathf.Atan2(input.y, -input.x) * Mathf.Rad2Deg;

        bool kB = false;
        // if (Gamepad.all[cIndex].buttonSouth.ReadValue() > 0) kB = true;
        if (Gamepad.all[cIndex].rightShoulder.ReadValue() > 0) kB = true;
        // if (usingKeyboard && Keyboard.current.kKey.ReadValue() > 0) kB = true;
        if (kB && knockbackCD.Completed) 
        {
            knockbackCD.Start();
            DoKnockback.Invoke();
        }
        knockbackCD.Tick();
    }

    void FixedUpdate()
    {
        // Rotation
        Quaternion aim = Quaternion.AngleAxis(aimAngle - 90, Vector3.up);
        knockbackPivot.rotation = aim;
        shieldPivot.rotation = Quaternion.RotateTowards(shieldPivot.rotation, aim, shieldRotateSpeed);
        timeCount += Time.deltaTime * shieldRotateSpeed;
    }

    public void PopOff()
    {
        transform.SetParent(null);
        transform.position += Vector3.up * 2;
        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.AddForce((Vector3.up * .25f) + new Vector3(Random.value, 0, Random.value), ForceMode.Impulse);
    }

    public void PickedUp()
    {
        transform.SetParent(team.runner.transform.GetChild(2));
        transform.localPosition = Vector3.zero;
        rb.detectCollisions = false;
        rb.isKinematic = true;
    }
}
