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
            Vector2 leftJoytstick = VR_Input.GetJoystickInput( XRNode.LeftHand );

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
            Vector2 rightJoystick = VR_Input.GetJoystickInput( XRNode.RightHand );
            scrollRect.velocity = rightJoystick * Time.deltaTime * scrollSpeed;
        }


    }

}

