using System;
using UnityEngine;

namespace FlappyBird3D
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bird : MonoBehaviour
    {
        [SerializeField] private PipeManager pipeManager;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxVelocityMagnitude = 5f;
        [SerializeField] private int scorePerPipe = 1;
        [SerializeField] private GameObject failScreen;
        
        private Vector3 _startPos = Vector3.zero;
        private Transform _bodyTransform = null;
        private Rigidbody _rigidbody = null;

        private void Start()
        {
            _bodyTransform = transform;
            _startPos = _bodyTransform.position;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //Rotate the body in the velocity direction
            _bodyTransform.rotation = Quaternion.LookRotation(_rigidbody.velocity + new Vector3(10f, 0f, 0f), transform.up);
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }

        /// <summary>
        /// Reset all parameters and start the game
        /// </summary>
        public void Restart()
        {
            failScreen.SetActive(false);
            pipeManager.canSpawn = true;
            _rigidbody.isKinematic = false;
            transform.position = _startPos;
            _rigidbody.velocity = Vector3.zero;
            pipeManager.ResetPipes();
            ScoreManager.instance.ResetScore();
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocityMagnitude);
        }

        /// <summary>
        /// Reset all parameters and stop the game
        /// </summary>
        private void Fail()
        {
            pipeManager.canSpawn = false;
            pipeManager.ResetPipes();
            _rigidbody.isKinematic = true;
            transform.position = _startPos;
            _rigidbody.velocity = Vector3.zero;
            failScreen.SetActive(true);
        }

        private void OnCollisionEnter(Collision other)
        {
            Fail();
        }

        private void OnTriggerExit(Collider other)
        {
            ScoreManager.instance.AddScore(scorePerPipe);
        }
    }
}
