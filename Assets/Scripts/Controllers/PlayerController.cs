using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Walk
// Run -- DONE
// Jump
// Animation

namespace VNEagleEngine.PlayerController
{
    //public enum ePlayerState
    //{
    //    IDLE,
    //    WALK,
    //    RUN,
    //    JUMP,
    //    FALL,
    //    ATTACK,
    //    SIT,
    //    CLIMB,
    //}

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _moveSpeed = 6.5f;
        [SerializeField] private float _accelerateSpeed = 12f;
        [SerializeField] private float _deccelerateSpeed = 12f;

        private Rigidbody2D _rig2d;
        //private ePlayerState _currentState = ePlayerState.IDLE;

        private void Awake()
        {
            _rig2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();

            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        // public void SetState(ePlayerState playerState) => _currentState = playerState;

        public void Jump()
        {
            _rig2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Move()
        {
            float rawAxis = Input.GetAxisRaw("Horizontal");
            float targetSpeed = rawAxis * _moveSpeed;
            float diffSpeed = targetSpeed - _rig2d.velocity.x;
            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? _accelerateSpeed : _deccelerateSpeed;
            float movement = diffSpeed * accelRate;

            _rig2d.AddForce(movement * Vector2.right);
        }
    }
}
