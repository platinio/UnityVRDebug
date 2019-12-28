using UnityEngine;

namespace VRDebug
{
    public class ConsoleCanvas : MonoBehaviour
    {
        [SerializeField] private Transform logContainer = null;
        [SerializeField] private LogCell logCellPrefab = null;
        

        private void Start()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string log , string stackTrace , LogType logType)
        {
            LogCell cell = Instantiate(logCellPrefab);
            cell.Construct(log);
        }
    }
}

