using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward, Space.Self);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hittable")
        {
            Destroy(this);
        }
    }
}
