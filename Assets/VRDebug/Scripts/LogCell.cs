using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRDebug
{

    public class LogCell : MonoBehaviour
    {
        [SerializeField] private Image icon = null;
        [SerializeField] private Image backGround = null;
        [SerializeField] private TextMeshProUGUI collapseCounterText = null;
        [SerializeField] private TextMeshProUGUI logText = null;
        [SerializeField] private TextMeshProUGUI stackTraceText = null;
        

        private int collapseCounter = 1;

        public int CollapseCounter
        {
            get
            {
                return collapseCounter;
            }
            set
            {
                collapseCounter = value;

                if (collapseCounter > 1)
                {
                    collapseCounterText.transform.parent.gameObject.SetActive( true );
                    collapseCounterText.text = value.ToString();
                }

            }
        }
        public LogType LogType { get; private set; }

        private void Awake()
        {
            collapseCounterText.transform.parent.gameObject.SetActive(false);
        }

        public void Construct(string log , string stackTrace , LogType logType, LogViewMode viewMode)
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
