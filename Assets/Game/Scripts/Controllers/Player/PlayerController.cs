using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Walk
// Run -- DONE
// Jump
// Animation

namespace VNEagleEngine.Player
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
        private const string ANIM_STATE_IS_JUMPING = "IsJumping";
        private const string ANIM_STATE_IS_MOVING = "IsMoving";
        private const string ANIM_STATE_IS_IN_COMBO = "IsInCombo";
        private const string ANIM_STATE_IS_GROUND = "IsGround";

        private const string ANIM_TRIGGER_JUMP = "Jump";
        private const string ANIM_TRIGGER_SWORD_ATTACK = "SwordAttack";


        [SerializeField] private float _jumpForce = 9f;
        [SerializeField] private float _moveSpeed = 6.5f;
        [SerializeField] private float _accelerateSpeed = 12f;
        [SerializeField] private float _deccelerateSpeed = 12f;

        [Header("Gravity Infomation")]
        [SerializeField] private float _initGravityScale = 2.0f;

        [Space]
        [Header("Controller Components")]
        [SerializeField] private PlayerTriggerBottom _triggerGroundController;


        [Space]
        [Header("Skill Components")]
        [SerializeField] private SkillContainer _skillContainer;
        [SerializeField] private GameObject _skillCasting;
        [SerializeField] private GameObject _targetingEnemy;

        private Animator _playerAnimator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rig2d;
        private bool _isGrounded = true;
        private bool _isBlockingInput = false;

        public GameObject SkillCastingContainer => _skillCasting;

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            float velocityY = _rig2d.velocity.y;

            // update status
            if (velocityY > 0 && !_isGrounded)
                this.SetAnimState(ANIM_STATE_IS_JUMPING, true);
            else
                this.SetAnimState(ANIM_STATE_IS_JUMPING, false);

            _playerAnimator.SetFloat("VelocityY", velocityY);
            
            if (Input.GetButtonDown("Jump") && _isGrounded && !GetAnimState(ANIM_STATE_IS_JUMPING))
                Jump();
            
            if (Input.GetKeyDown(KeyCode.Q))
                CastSkillAtSlot(0);
            if (Input.GetKeyDown(KeyCode.E))
                CastSkillAtSlot(1);
        }

        private void FixedUpdate()
        {
            Move();
            AddForceWhenFall();
        }

        private void Init()
        {
            _rig2d = GetComponent<Rigidbody2D>();
            _playerAnimator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _triggerGroundController.Init(this);
            _skillContainer.Init(this);
            _rig2d.gravityScale = _initGravityScale;
        }

        // public void SetState(ePlayerState playerState) => _currentState = playerState;

        private void Jump()
        {
            this.SetAnimTrigger(ANIM_TRIGGER_JUMP);
            _rig2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void Move()
        {
            float rawAxis = Input.GetAxisRaw("Horizontal");
            if (rawAxis == 0)
            {
                this.SetAnimState(ANIM_STATE_IS_MOVING, false);
            }
            else if (rawAxis < 0)
            {
                this.SetAnimState(ANIM_STATE_IS_MOVING, true);
                this._spriteRenderer.flipX = true;
            }
            else
            {
                this.SetAnimState(ANIM_STATE_IS_MOVING, true);
                this._spriteRenderer.flipX = false;
            }

            float targetSpeed = rawAxis * _moveSpeed;
            float diffSpeed = targetSpeed - _rig2d.velocity.x;

            if (diffSpeed == 0)
                return;

            float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? _accelerateSpeed : _deccelerateSpeed;
            float movement = diffSpeed * accelRate;

            _rig2d.AddForce(movement * Vector2.right);
        }

        private void AddForceWhenFall()
        {
            if (_rig2d.velocity.y < 0 || this.GetAnimState(ANIM_STATE_IS_JUMPING))
            {
            }
        }

        public void OnDetectGround()
        {
            this._isGrounded = true;
            this.SetAnimState(ANIM_STATE_IS_GROUND, true);
            _playerAnimator.ResetTrigger(ANIM_TRIGGER_JUMP);
        }

        public void OnLeaveGround()
        {
            this._isGrounded = false;
            this.SetAnimState(ANIM_STATE_IS_GROUND, false);
        }

        public void SetAnimState(string state, bool value)
        {
            _playerAnimator.SetBool(state, value);
        }

        public bool GetAnimState(string state)
        {
            return _playerAnimator.GetBool(state);
        }

        public void SetAnimTrigger(string trigger)
        {
            _playerAnimator.SetTrigger(trigger);
        }

        public Animator GetPlayerAnimator()
        {
            return this._playerAnimator;
        }

        public void CastSkillAtSlot(int slot)
        {
            var skill = _skillContainer.GetSkillAtSlot(slot);
            if (skill != null)
                skill.Cast();
        }

        public void ToggleBlockingInput(bool isOn)
        {
            this._isBlockingInput = isOn;
        }

        public bool IsPlayerRightDirection => !this._spriteRenderer.flipX;
    }
}
