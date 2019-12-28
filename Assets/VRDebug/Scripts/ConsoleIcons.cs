using UnityEngine;
using UnityEngine.UI;

namespace VRDebug
{
    public class ConsoleIcons : MonoBehaviour
    {
        [SerializeField] private ConsoleCanvas consoleCanvas = null;
        [SerializeField] private Image logBg = null;
        [SerializeField] private Image errorBg = null;
        [SerializeField] private Image warningBg = null;

        public void OnLogFilterChange()
        {
            if (consoleCanvas.CurrentLogFilter == LogFilter.All)
            {
                logBg.enabled = true;
                errorBg.enabled = true;
                warningBg.enabled = true;
            }
            else if (consoleCanvas.CurrentLogFilter == LogFilter.Error)
            {
                errorBg.enabled = true;
                logBg.enabled = false;
                warningBg.enabled = false;
            }
            else if (consoleCanvas.CurrentLogFilter == LogFilter.Log)
            {
                errorBg.enabled = false;
                logBg.enabled = true;
                warningBg.enabled = false;
            }
            else
            {
                errorBg.enabled = false;
                logBg.enabled = false;
                warningBg.enabled = true;
            }
        }


    }

}

