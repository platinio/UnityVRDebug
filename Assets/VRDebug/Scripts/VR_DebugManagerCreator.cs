using UnityEngine;

namespace VRDebug
{
    public class VR_DebugManagerCreator 
    {
        [RuntimeInitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            GameObject go = Resources.Load("VR_DebugManager") as GameObject;
            GameObject.Instantiate(go);
        }
    }
}

