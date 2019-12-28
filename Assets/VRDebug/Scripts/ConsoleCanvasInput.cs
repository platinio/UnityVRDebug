using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

namespace VRDebug
{
    public class ConsoleCanvasInput : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed = 800.0f;
        [SerializeField] private ScrollRect scrollRect = null;

        private ConsoleCanvas consoleCanvas = null;
        private InputDevice rightHand;
        private InputDevice leftHand;

        private bool waitJoystickZero = false;

        private void Awake()
        {
            consoleCanvas = GetComponent<ConsoleCanvas>();
        }

        private void Update()
        {
            UpdateLogFilterInput();
            UpdateScrollInput();
        }

        private void UpdateLogFilterInput()
        {
            Vector2 leftJoytstick = GetJoystickInput( XRNode.LeftHand );

            if (!waitJoystickZero && Mathf.Abs( leftJoytstick.x ) > 0.25f)
            {
                consoleCanvas.MoveLogFilter( leftJoytstick.x > 0.0f ? 1 : -1 );
                waitJoystickZero = true;
            }

            else if (waitJoystickZero && leftJoytstick.magnitude < 0.1f)
            {
                waitJoystickZero = false;
            }
        }

        private void UpdateScrollInput()
        {
            Vector2 rightJoystick = GetJoystickInput( XRNode.RightHand );
            scrollRect.velocity = rightJoystick * Time.deltaTime * scrollSpeed;
        }

        private Vector2 GetJoystickInput( XRNode node )
        {
            InputDevice hand = GetHand(node);

            Vector2 input = Vector2.zero;
            hand.TryGetFeatureValue( CommonUsages.primary2DAxis , out input );
            return input;
        }

        private InputDevice GetHand(XRNode node)
        {
            if (node == XRNode.RightHand && !rightHand.isValid)
            {
                rightHand = InputDevices.GetDeviceAtXRNode(node);
                return rightHand;
            }
            if (node  == XRNode.LeftHand)
            {
                leftHand = InputDevices.GetDeviceAtXRNode(node);
                return leftHand;
            }

            return rightHand;
        }

    }

}

