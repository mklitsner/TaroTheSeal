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
            SealControl seal;
            if(other.TryGetComponent<SealControl>(out seal))
            {
                isFlownThrough = true;
                buildingPart.FixDamage(seal.spinAction);
            }
           
        }
    }

    public void onReset()
    {
        isFlownThrough = false;
    }
}
