using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsSettings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 100; //For a target fps of 100.
    }
}
