using UnityEngine;
using Platinio;
using UnityEngine.XR;
using System.Collections.Generic;

namespace VRDebug
{
    public class VR_DebugManager : Singleton<VR_DebugManager>
    {
        [SerializeField] private ConsoleCanvas consolePrefab = null;

        private InputDevice rightHand;
        private List<InputDevice> inputDeviceList = new List<InputDevice>();
        private bool waitButtonUp = false;
        private float timer = 0.0f;
        private const float buttonPressedTime = 3.0f;

        protected override void Awake()
        {
            base.Awake();
            rightHand = InputDevices.GetDeviceAtXRNode( XRNode.RightHand );

            
        }

        private void Update()
        {
            if (VR_Input.GetPrimaryButtonDown( XRNode.LeftHand ) && VR_Input.GetPrimaryButtonDown( XRNode.RightHand ))
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0.0f;
                waitButtonUp = false;
            }

            if ( timer > buttonPressedTime && !waitButtonUp)
            {
                DestroyAllConsoles();
                CreateConsole();
                waitButtonUp = true;
            }

        }
        
        private void CreateConsole()
        {
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            Camera camera = Camera.main;

            if (camera != null)
            {
                Debug.Log("log camera main no null");
                position = camera.transform.position + camera.transform.forward * 8.0f;
                rotation = Quaternion.LookRotation( camera.transform.forward  );
            }

            Instantiate(consolePrefab , position , rotation);
        }

        private void DestroyAllConsoles()
        {
            ConsoleCanvas[] consoleCanvasArray = FindObjectsOfType<ConsoleCanvas>();

            for (int n = 0; n < consoleCanvasArray.Length; n++)
            {
                Destroy( consoleCanvasArray[n].gameObject );
            }
        }

    }
}
