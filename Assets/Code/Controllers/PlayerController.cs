using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : Controller
{
    public Vector3 currentAimPoint;
    //public int Lives { get; private set; } = 3;
    public Transform Cam { get; private set; }

    private TankMovement TankMovement;

    protected override void Awake()
    {
        base.Awake();
        pawn.Health.DeathEvent += OnDeath;
        TankMovement = GetComponent<TankMovement>();
    }

    private void OnDeath(TankPawn killer)
    {
        GameManager.Instance.EndGame();
    }

    public void TakeControl(Pawn controlledPawn, int playerNum = 0)
    {
        pawn = controlledPawn;

        CameraManager.Instance.Attach(GetComponent<TankPawn>());
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    private void OnDestroy()
    {
        if (Cam != null)
            Cam.transform.parent = null;
    }

    public void Update()
    {
        if (pawn == null) return;
        ProcessInputs();
        
        if(UI.IsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public override void ProcessInputs()
    {
        var move = GameManager.Controls.FindAction("Move").ReadValue<Vector2>();
        var look = GameManager.Controls.FindAction("Look").ReadValue<Vector2>();
        
        if (pawn.Health.CurrentHealth <= 0)
        {
            move = Vector2.zero;
            look = Vector2.zero;
        }

        pawn.HullRotate(move.x);
        pawn.HullMove(move.y);

        pawn.TurretRotate(look.x);
        pawn.TurretPitch(look.y);

        ShootInput();
    }

    private void ShootInput()
    {
        var shoot = GameManager.Controls.FindAction("Shoot").WasPressedThisFrame();

        if(shoot)
        {
            pawn.Shoot();
        }
    }
}
