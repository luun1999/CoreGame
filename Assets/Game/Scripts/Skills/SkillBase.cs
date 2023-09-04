using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNEagleEngine.Player;

namespace VNEagleEngine
{
    public enum AnimationType
    {
        NONE,
        CAST,
    }

    public abstract class SkillBase : MonoBehaviour
    {
        [SerializeField] protected float _coolDown = 1f; // default cooldown is 1 second
        [SerializeField] protected AnimationType _animationType; // animation when casting skill
        [SerializeField] protected bool _isSkillBlockInput = false;

        protected PlayerController _player;
        protected Animator _playerAnimator;
        protected float _coolDownTimer = 0f;

        public bool CanCastSkill => this._coolDownTimer <= 0;

        protected void Update()
        {
            if (_coolDownTimer > 0)
            {
                _coolDownTimer -= Time.deltaTime;
                if (_coolDownTimer < 0)
                    _coolDownTimer = 0f;
            }
        }

        public void SetCoolDown()
        {
            this._coolDownTimer = this._coolDown;
        }

        public void Init(PlayerController player)
        {
            if (player != null)
            {
                this._playerAnimator = player.GetPlayerAnimator();
                this._player = player;
            }
        }

        public virtual void Cast() 
        {
            if (this._isSkillBlockInput)
                _player.ToggleBlockingInput(true);

            // get animation info 
            // wait end animation
            // unlock blocking input
            switch(_animationType)
            {
                case AnimationType.CAST:
                    _playerAnimator.Play("Cast");
                    break;
                default:
                    break;
            }
        }
    }
}
