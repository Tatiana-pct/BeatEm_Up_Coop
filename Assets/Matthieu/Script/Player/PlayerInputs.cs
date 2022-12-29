using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region Gestion Input Axes Vertical / Horizontal
    // Axe deplacement
    private Vector2 _movement;

    public Vector2 Movement { get => _movement; }
    public Vector2 NormalizedMovement { get => _movement.normalized; }
    public Vector2 ClampedMovement { get => Vector2.ClampMagnitude(_movement, 1f); }
    public bool HasMovement { get => _movement != Vector2.zero; }
    #endregion

    #region Gestion Input autre
    private bool _askingRunning;
    private bool _askingJumping;
    private bool _askingAttack;

    private bool _askingDebugButton;

    public bool AskingRunning { get => _askingRunning; }
    public bool AskingJumping { get => _askingJumping; }
    public bool AskingAttack { get => _askingAttack; }
    public bool AskingDebugButton { get => _askingDebugButton; }
    #endregion

    private void Update()
    {
        // Stockage Mouvement
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Stockage inputs
        _askingRunning = Input.GetKey(KeyCode.LeftShift);
        _askingJumping = Input.GetKeyDown(KeyCode.Space);
        _askingAttack = Input.GetKeyDown(KeyCode.Mouse0);
        _askingDebugButton = Input.GetKeyDown(KeyCode.F1);
    }
}
