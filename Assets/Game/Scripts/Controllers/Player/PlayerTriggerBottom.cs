using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine.Player
{
    public class PlayerTriggerBottom : MonoBehaviour
    {
        private PlayerController _playerController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
             if (!collision.gameObject.CompareTag("Ground") || _playerController == null) return;

            _playerController.OnDetectGround();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground") || _playerController == null) return;

            _playerController.OnDetectGround();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground") || _playerController == null) return;

            _playerController.OnLeaveGround();
        }

        public void Init(PlayerController playerController)
        {
            _playerController = playerController;
        }
    }
}
