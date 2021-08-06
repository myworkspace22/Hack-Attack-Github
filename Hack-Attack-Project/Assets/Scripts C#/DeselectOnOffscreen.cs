using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectOnOffscreen : MonoBehaviour
{
    public BuildManager byggemandbob;
    private void OnMouseDown()
    {
        if (!byggemandbob.CanBuild)
        {
            byggemandbob.DeselectNode();
            return;
        }
    }
}
