using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIgloo : MonoBehaviour
{
    [Range(0f, 360f)]
    public float angleRange = 45f;

    [Range(0f, 10f)]
    public float speed = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        // if(Input.GetKeyDown("space"))
        // {
        //     StartCoroutine(RandomRotate());
        // }
    }

    public void CallRotate()
    {
        StartCoroutine(RandomRotate());
    }

    IEnumerator RandomRotate()
    {
        float randomAngle = Random.Range(5f, angleRange);
        float randomDir = Random.Range(0f, 1f) < 0.5f ? -1 : 1;
        float currAngle = 0;
        while(currAngle < randomAngle)
        {
            transform.Rotate(0f, speed * randomDir, 0f, Space.World);
            currAngle += speed;
            yield return null;
        }
    }
}
