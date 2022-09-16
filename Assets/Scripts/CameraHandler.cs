using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraHandler : MonoBehaviour
{
    public Image cameraBorder;
    public PlayerController parent;

    public void Start()
    {
        parent = transform.parent.GetComponent<PlayerController>();
    }


    public void SetBorderColor(Color c)
    {
        cameraBorder.color = c;
    }
}
