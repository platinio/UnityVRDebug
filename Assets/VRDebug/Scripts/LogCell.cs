using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRDebug
{

    public class LogCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text = null;

        public void Construct(string label)
        {
            text.text = label;
        }
    }

}
