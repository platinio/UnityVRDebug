using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRDebug
{

    public class LogCell : MonoBehaviour
    {
        [SerializeField] private Image icon = null;
        [SerializeField] private Image backGround = null;
        [SerializeField] private TextMeshProUGUI logText = null;
        [SerializeField] private TextMeshProUGUI stackTraceText = null;

        private int collapseCounter = 0;

        public int CollapseCounter { get { return collapseCounter; } set { } }
        public LogType LogType { get; private set; }
        

        public void Construct(string log , string stackTrace , LogViewMode viewMode , LogType logType)
        {
            LogType = logType;
            logText.text = log;
            stackTraceText.text = stackTrace;

            ApplyLogViewMode(viewMode);
        }

        private void ApplyLogViewMode(LogViewMode viewMode)
        {
            icon.sprite = viewMode.icon;
            backGround.color = viewMode.bgColor;
            logText.color = viewMode.textColor;
            stackTraceText.color = viewMode.textColor;
        }




    }

}
