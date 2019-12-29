using UnityEngine;
using UnityEngine.XR;

namespace VRDebug
{
    public static class VR_Input 
    {
        private static InputDevice rightHand;
        private static InputDevice leftHand;


        public static Vector2 GetJoystickInput(XRNode node)
        {
            InputDevice hand = GetHand( node );

            Vector2 input = Vector2.zero;
            hand.TryGetFeatureValue( CommonUsages.primary2DAxis, out input );
            return input;
        }

        public static bool GetPrimaryButtonDown(XRNode node)
        {
            InputDevice hand = GetHand( node );

            bool value = false;
            hand.TryGetFeatureValue( CommonUsages.primaryButton, out value );
            return value;
        }

        private static InputDevice GetHand(XRNode node)
        {
            if (node == XRNode.RightHand && !rightHand.isValid)
            {
                rightHand = InputDevices.GetDeviceAtXRNode( node );
                return rightHand;
            }
            if (node == XRNode.LeftHand)
            {
                leftHand = InputDevices.GetDeviceAtXRNode( node );
                return leftHand;
            }

            return rightHand;
        }



    }

}

