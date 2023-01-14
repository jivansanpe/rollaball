using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public float JumpSpeed = 0.2f;
    private Rigidbody rb;
    public float thrust = 5;
    bool m_isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_isGrounded = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded == true)
        {
            Jump();
        }
    }

    public void Jump()
    {
        m_isGrounded = false;
        rb.AddForce(0, thrust, 0, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

}
