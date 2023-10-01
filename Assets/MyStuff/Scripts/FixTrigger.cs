using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTrigger : MonoBehaviour
{

    public bool isFlownThrough;
 [SerializeField] BuildingPart buildingPart;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isFlownThrough = true;
            buildingPart.FixDamage();
        }
    }

    public void onReset()
    {
        isFlownThrough = false;
    }
}
