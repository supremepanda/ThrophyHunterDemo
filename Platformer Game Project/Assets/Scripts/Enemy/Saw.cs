﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, 0f, -80f * Time.deltaTime);
    }

}
