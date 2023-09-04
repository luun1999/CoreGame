using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine
{
    public class LightHole : SkillBase
    {
        [SerializeField] private Transform _circle;

        public override void Cast()
        {
            if (!CanCastSkill)
                return;
            base.Cast();
            SetCoolDown();

            // show white circle
            StartCoroutine(Cor_LightBallLinear());
        }

        IEnumerator Cor_LightBallLinear()
        {
            var castCircle = Instantiate(_circle, _player.SkillCastingContainer.transform);
            castCircle.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + 1);
            castCircle.gameObject.SetActive(true);

            float playerPositionX = castCircle.transform.position.x;

            if (_player.IsPlayerRightDirection)
            {
                var targetX = playerPositionX + 10f;
                while (playerPositionX < targetX)
                {
                    playerPositionX += 0.01f;
                    castCircle.transform.position = new Vector3(playerPositionX, castCircle.transform.position.y, castCircle.transform.position.z);
                    yield return null;
                }
            }
            else
            {
                var targetX = playerPositionX - 10f;
                while (playerPositionX > targetX)
                {
                    playerPositionX -= 0.01f;
                    castCircle.transform.position = new Vector3(playerPositionX, castCircle.transform.position.y, castCircle.transform.position.z);
                    yield return null;
                }
            }

            castCircle.gameObject.SetActive(false);
            Destroy(castCircle.gameObject);
        }
    }
}
