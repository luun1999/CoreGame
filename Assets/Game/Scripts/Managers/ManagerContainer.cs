using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine.Manager
{
    public class ManagerContainer : MonoBehaviour
    {   
        [System.Serializable]
        public enum ePermissionType
        {
            Camera,
            Microphone,
            SpeechRecognition
        }

        [System.Serializable]
        public class PermissionPopup
        {
            public string permissionLabel;
            public ePermissionType permissionType;
            public GameObject permissionPanel;
        }

        [SerializeField] PermissionPopup[] permissionPopups;
        [SerializeField] GameObject[] managers;

        private void Awake()
        {
            foreach (var manager in managers)
            {
                Instantiate(manager, this.transform);
            }
        }

        public void EnablePermissionPopup(ePermissionType permissionType, bool isEnable)
        {
            foreach (var popupItem in permissionPopups)
            {
                if (popupItem.permissionType == permissionType)
                {
                    popupItem.permissionPanel.SetActive(!popupItem.permissionPanel.activeSelf);
                    break;
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                EnablePermissionPopup(ePermissionType.Camera, true);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                EnablePermissionPopup(ePermissionType.Microphone, true);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                EnablePermissionPopup(ePermissionType.SpeechRecognition, true);
        }
    }
}
