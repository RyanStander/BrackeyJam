using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float MoveSpeed;
    public float Walkspeed = 2f;
    public float SprintSpeedMultiplier = 1.5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _movement;
    private bool _jumpRequested;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        //_movement.y = Input.GetAxisRaw("Vertical");

        if (animator != null)
        {
            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Vertical", _movement.y);
            animator.SetFloat("Speed", _movement.sqrMagnitude);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveSpeed = Walkspeed * SprintSpeedMultiplier;

        }
        else
        {
            MoveSpeed = Walkspeed;
        }

       
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * MoveSpeed * Time.fixedDeltaTime);

  
    }
}
