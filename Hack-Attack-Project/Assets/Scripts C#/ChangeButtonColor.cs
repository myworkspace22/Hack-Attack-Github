using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    public Image changeColor;
    
    public void OnMouseEnter()
    {
        changeColor.color = new Color(0.2f, 0.2f, 0.2f, 1);
    }
}
