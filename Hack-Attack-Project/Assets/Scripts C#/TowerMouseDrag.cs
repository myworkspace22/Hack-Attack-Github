using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMouseDrag : MonoBehaviour
{
    private SpriteRenderer towerIcon;

    private BuildManager buildManager;

    //private bool enabledIcon;

    private void Start()
    {
        buildManager = BuildManager.instance;

        towerIcon = GetComponent<SpriteRenderer>();

        //enabledIcon = false;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up, 0.1f, ~7);
        if(buildManager.CanBuild)
        {
            if (hit)
            {
                Node n = hit.transform.GetComponent<Node>();
                if (n != null)
                {
                    if (towerIcon.enabled)
                    {
                        towerIcon.enabled = false;
                    }
                    return;
                }
            }
            if (!towerIcon.enabled)
            {
                towerIcon.enabled = true;
            }
            UpdateTowerIcon();
        }
        else
        {
            if (towerIcon.enabled)
            {
                towerIcon.enabled = false;
            }
        }
    }

    private void UpdateTowerIcon()
    {
        towerIcon.sprite = buildManager.GetTurretToBuild().prefab.GetComponent<SpriteRenderer>().sprite;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //enabledIcon = false;
    //}
}
