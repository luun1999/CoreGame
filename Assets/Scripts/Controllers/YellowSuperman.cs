using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Superman.Controller
{
    public class YellowSuperman : MonoBehaviour
    {
        [SerializeField] private float force = 1f;
        [SerializeField] private float speed = 1f;


        private Rigidbody2D rig2d;
        private SpriteRenderer renderer;
        private float moveInput;
        private bool isGround = false;

        private void Awake()
        {
            rig2d = GetComponent<Rigidbody2D>();
            renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJump();
            }


            moveInput = Input.GetAxis("Horizontal");
            OnMove();
        }

        private void OnJump()
        {
            if (isGround)
                rig2d.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        private void OnMove()
        {
            if (moveInput < 0)
                renderer.flipX = true;
            else if (moveInput > 0)
                renderer.flipX = false;
            transform.position += new Vector3(speed * moveInput * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground"))
            {
                isGround = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground"))
            {
                isGround = false;
            }
        }
    }
}

