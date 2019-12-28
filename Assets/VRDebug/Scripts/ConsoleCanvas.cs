using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace VRDebug
{

    public enum LogFilter
    {
        Log = 0,
        Warning = 1,
        Error = 2,
        All = 3
    }

    public class ConsoleCanvas : MonoBehaviour
    {
        [SerializeField] private Transform logContainer = null;
        [SerializeField] private LogCell logCellPrefab = null;       
        [SerializeField] private Scroller scroller = null;        
        [SerializeField] private LogViewMode debugLogViewMode;
        [SerializeField] private LogViewMode errorLogViewMode;
        [SerializeField] private LogViewMode warningLogViewMode;
        [SerializeField] private UnityEvent OnLogFilterChange = null;

        private Dictionary<string, LogCell> logCellDictonary = new Dictionary<string, LogCell>();        

        public LogFilter CurrentLogFilter { get; private set; }
        
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
                    Debug.Log( "Log test" + n );
                }
                else if (rand <= 0.66)
                {
                    Debug.LogWarning( "Warning test" + n );
                }
                else
                {
                    Debug.LogError("Error test" + n);
                }

            }
        }

        public void MoveLogFilter(int dir)
        {
            int enumValue = ( (int) CurrentLogFilter ) + dir;

            if (enumValue < 0)
                enumValue = 3;
            if (enumValue > 3)
                enumValue = 0;

            CurrentLogFilter = (LogFilter) enumValue;

            OnUpdateLogFilter();
            OnLogFilterChange.Invoke();
        }

        public void OnUpdateLogFilter()
        {
            List<LogCell> cellList = logCellDictonary.Values.ToList();

            for (int n = 0; n < cellList.Count; n++)
            {
                LogCell cell = cellList[n];
                cellList[n].gameObject.SetActive( CanRenderLogType( cell.LogType ) );
            }
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void HandleLog(string log, string stackTrace, LogType logType)
        {

            LogCell cell = null;

            if (logCellDictonary.TryGetValue( stackTrace + log, out cell ))
            {
                cell.CollapseCounter++;
            }
            else
            {
                cell = CreateLogCell();
                cell.Construct( log, stackTrace, logType , GetLogViewMode( logType ) );
                logCellDictonary[stackTrace + log] = cell;
                scroller.AddElement( cell.gameObject );

                if (!CanRenderLogType( logType ))
                {
                    cell.gameObject.SetActive(false);
                }

            }            

        }
                

        private bool CanRenderLogType(LogType logType)
        {
            if (CurrentLogFilter == LogFilter.All)
                return true;

            if (logType == LogType.Error || logType == LogType.Exception)
                return CurrentLogFilter == LogFilter.Error;
            if (logType == LogType.Warning)
                return CurrentLogFilter == LogFilter.Warning;
            if (logType == LogType.Log)
                return CurrentLogFilter == LogFilter.Log;
            
            return false;
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

