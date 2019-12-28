using UnityEngine;

namespace VRDebug
{
    public class ConsoleCanvas : MonoBehaviour
    {
        [SerializeField] private Transform logContainer = null;
        [SerializeField] private LogCell logCellPrefab = null;
        [SerializeField] private LogViewMode debugLogViewMode;
        [SerializeField] private LogViewMode errorLogViewMode;
        [SerializeField] private LogViewMode warningLogViewMode;


        private void Awake()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void Start()
        {
            for (int n = 0; n < 10; n++)
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

        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string log, string stackTrace, LogType logType)
        {
            LogCell cell = Instantiate( logCellPrefab );
            cell.Construct( log, stackTrace, GetLogViewMode(logType) );
            cell.transform.parent = logContainer;
            cell.transform.localScale = Vector3.one;
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

