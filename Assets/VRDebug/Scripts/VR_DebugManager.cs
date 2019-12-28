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

        protected override void Awake()
        {
            base.Awake();
            rightHand = InputDevices.GetDeviceAtXRNode( XRNode.RightHand );

            
        }

        private void Update()
        {
            if (ShouldCreateConsole() && !IsConsoleActive())
            {
                CreateConsole();
            }

        }

        private bool IsConsoleActive()
        {
            return FindObjectOfType<ConsoleCanvas>() != null;
        }

        private bool ShouldCreateConsole()
        {
            rightHand = InputDevices.GetDeviceAtXRNode( XRNode.RightHand );
            
            bool value = false;
            rightHand.TryGetFeatureValue( CommonUsages.triggerButton, out value );
            return value;
        }

        private void CreateConsole()
        {
            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            Camera camera = Camera.main;

            if (camera != null)
            {
                position = camera.transform.position + camera.transform.forward * 8.0f;
                rotation = Quaternion.LookRotation( camera.transform.forward * -1.0f );
            }

            Instantiate(consolePrefab , position , rotation);
        }

    }
}
