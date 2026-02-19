using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float PlayerMoveSpeed;
    public float PlayerWalkSpeed = 2f;
    public float PlayerSprintSpeedMultiplier = 1.5f;
    public Rigidbody2D PlayerRB;

    public Vector2 CurrentMovement { get; private set; }
    public bool IsSprinting { get; private set; }

    void Start()
    {
        if (PlayerRB == null) PlayerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        CurrentMovement = new Vector2(x, y).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerMoveSpeed = PlayerWalkSpeed * PlayerSprintSpeedMultiplier;
            IsSprinting = true;
        }
        else
        {
            PlayerMoveSpeed = PlayerWalkSpeed;
            IsSprinting = false;
        }
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + CurrentMovement * PlayerMoveSpeed * Time.fixedDeltaTime);
    }
}