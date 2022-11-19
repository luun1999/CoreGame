using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine.Manager
{
    public class ManagerContainer : MonoBehaviour
    {
        [SerializeField] GameObject[] managers;

        private void Awake()
        {
            foreach (var manager in managers)
            {
                Instantiate(manager, this.transform);
            }
        }
    }
}
