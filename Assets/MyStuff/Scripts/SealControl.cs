using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SealControl : MonoBehaviour
{
    public UnityEvent onTap;

    void Update()
    {
        // Check if the screen is tapped or clicked
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button or a single touch tap
        {
            onTap.Invoke(); // Trigger the UnityEvent
        }
    }
}
