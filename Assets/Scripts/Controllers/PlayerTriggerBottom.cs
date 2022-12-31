using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine.PlayerController
{
    public class PlayerTriggerBottom : MonoBehaviour
    {
        private PlayerController _playerController;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground") || _playerController == null) return;

             
        }

        public void Init(PlayerController playerController)
        {
            this._playerController = playerController;
        }
    }
}
