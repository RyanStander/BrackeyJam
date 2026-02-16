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

    private Vector2 _movement;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerRB == null)
        {
            PlayerRB = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement = _movement.normalized;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerMoveSpeed = PlayerWalkSpeed * PlayerSprintSpeedMultiplier;
        }
        else
        {
            PlayerMoveSpeed = PlayerWalkSpeed;
        }
    }

    private void FixedUpdate()
    {
        PlayerRB.MovePosition(PlayerRB.position + _movement * PlayerMoveSpeed * Time.fixedDeltaTime);
    }
}
