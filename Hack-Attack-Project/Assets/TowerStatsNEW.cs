using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStatsNEW : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public float xPos = 0;
    public float yPos = 0;
    public float zPos = -2;

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();
        transform.position += new Vector3(xPos, yPos, zPos);

        ui.SetActive(true);
    }
    public void Hide()
    {
        ui.SetActive(false);
    }
}
