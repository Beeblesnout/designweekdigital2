using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Popcron.Console;

public class Buddy : MonoBehaviour
{
    #region VARIABLES

        public int teamID;
        public TeamPlayer teamPlayer;
        public Transform body;

        #region ABILITIES
            [Header("Abilities")]
            [Command("kb_str")]
            public float knockbackStrength;
        #endregion

        #region CONTROLS
            [Header("Controls")]
            /// <summary>
            /// The index of this player's controller.
            /// </summary>
            public int cIndex;
        #endregion

    #endregion

    void Awake()
    {
        
    }
    
    void OnEnable() { Parser.Register(this, "buddy" + teamID); }
    void OnDisable() { Parser.Unregister(this); }

    void Start()
    {
        teamPlayer = GetComponent<TeamPlayer>();
        
    }

    void Update()
    {
        float aimAngle = Mathf.Atan2(Input.GetAxis("Vertical" + cIndex), Input.GetAxis("Horizontal" + cIndex)) * Mathf.Rad2Deg;
        body.rotation = Quaternion.AngleAxis(aimAngle, Vector3.up);
    }

    void FixedUpdate()
    {
        
    }
}
