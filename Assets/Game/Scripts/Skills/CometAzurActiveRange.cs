using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine
{
    public class CometAzurActiveRange : MonoBehaviour
    {
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Attack enemy");
                collision.gameObject.SetActive(false);
            }
        }
    }
}

