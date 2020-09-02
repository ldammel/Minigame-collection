using System;
using UnityEngine;

namespace Platformer.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxVelocityMagnitude = 5f;

        private Rigidbody _rb;
        private bool _isGrounded;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            
            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) Jump();
        }

        private void Move()
        {
            var h = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            transform.Translate(h,0,0);
        }
        
        private void Jump()
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocityMagnitude);
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.collider.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.CompareTag("Ground"))
            {
                _isGrounded = false;
            }
        }
    }
}
