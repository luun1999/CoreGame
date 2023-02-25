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
        [SerializeField] private float _jumpForce = 9f;
        [SerializeField] private float _moveSpeed = 6.5f;
        [SerializeField] private float _accelerateSpeed = 12f;
        [SerializeField] private float _deccelerateSpeed = 12f;

        [Header("Gravity Infomation")]
        [SerializeField] private float _initGravityScale = 2.0f;

        [Space]
        [Header("Controller Components")]
        [SerializeField] private PlayerTriggerBottom _triggerGroundController;

        private Rigidbody2D _rig2d;
        private PlayerState _playerState;
        //private ePlayerState _currentState = ePlayerState.IDLE;

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && _playerState.IsGround)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            Move();
            AddForceWhenFall();
        }

        private void Init()
        {
            _rig2d = GetComponent<Rigidbody2D>();

            _triggerGroundController.Init(this);

            _playerState = new PlayerState();
            _rig2d.gravityScale = _initGravityScale;
        }

        // public void SetState(ePlayerState playerState) => _currentState = playerState;

        private void Jump()
        {
            _playerState.IsJumping = true;
            _rig2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void Move()
        {
            float rawAxis = Input.GetAxisRaw("Horizontal");
            float targetSpeed = rawAxis * _moveSpeed;
            float diffSpeed = targetSpeed - _rig2d.velocity.x;

            if (diffSpeed == 0) return;

            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? _accelerateSpeed : _deccelerateSpeed;
            float movement = diffSpeed * accelRate;

            _rig2d.AddForce(movement * Vector2.right);
        }

        private void AddForceWhenFall()
        {
            if (_rig2d.velocity.y < 0 || _playerState.IsJumping)
            {
            }
        }

        public void OnDetectGround()
        {
            _playerState.IsJumping = false;
            _playerState.IsGround = true;
        }

        public void OnLeaveGround()
        {
            _playerState.IsGround = false;
        }
    }

    public class PlayerState
    {
        public bool IsJumping;
        public bool IsGround;

        public PlayerState()
        {
            IsJumping = false;
            IsGround = true;
        }
    }
}
