using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.Collections.Generic;

namespace VRDebug
{
   
    public class ConsoleCanvas : MonoBehaviour
    {
        [SerializeField] private Transform logContainer = null;
        [SerializeField] private LogCell logCellPrefab = null;
        [SerializeField] private ScrollRect scrollRect = null;
        [SerializeField] private Scroller scroller = null;
        [SerializeField] private float scrollSpeed = 200.0f;
        [SerializeField] private LogViewMode debugLogViewMode;
        [SerializeField] private LogViewMode errorLogViewMode;
        [SerializeField] private LogViewMode warningLogViewMode;

        private List<LogCell> logCellList = new List<LogCell>();
        [SerializeField] private bool showErrors = false;
        [SerializeField] private bool showDebugs = false;
        [SerializeField] private bool showWarnings = false;

        private void Awake()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void Start()
        {
            for (int n = 0; n < 100; n++)
            {
                float rand = Random.value;

                if (rand <= 0.33)
                {
                    Debug.Log( "Log test" );
                }
                else if (rand <= 0.66)
                {
                    Debug.LogWarning( "Warning test" );
                }
                else
                {
                    Debug.LogError("Error test");
                }

            }
        }

        private void Update()
        {
            InputDevice rightHand = InputDevices.GetDeviceAtXRNode( XRNode.RightHand );

            Vector2 value = Vector2.zero;
            rightHand.TryGetFeatureValue( CommonUsages.primary2DAxis, out value );

           
            scrollRect.velocity = value * scrollSpeed * Time.deltaTime;
           
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string log, string stackTrace, LogType logType)
        {
            
            LogCell cell = CreateLogCell();
            cell.Construct( log, stackTrace, GetLogViewMode(logType) );            
        
            scroller.AddElement(cell.gameObject);
            logCellList.Add(cell); 

            //cell.gameObject.SetActive( logType );

        }

        private LogCell CreateLogCell()
        {
            LogCell cell = Instantiate( logCellPrefab );           
            cell.transform.parent = logContainer;
            cell.transform.localScale = Vector3.one;

            return cell;
        }


        private LogViewMode GetLogViewMode(LogType logType)
        {
            if (logType == LogType.Exception || logType == LogType.Error)
                return errorLogViewMode;
            else if (logType == LogType.Warning)
                return warningLogViewMode;

            return debugLogViewMode;
        }

    }

    [System.Serializable]
    public struct LogViewMode
    {
        public Sprite icon;
        public Color textColor;
        public Color bgColor;
    }

}

