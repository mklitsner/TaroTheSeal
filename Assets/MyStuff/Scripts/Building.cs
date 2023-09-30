using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Building : MonoBehaviour
{
    public List<BuildingPart> BuildingParts;

    private void Start()
    {
        BuildingParts = GetComponentsInChildren<BuildingPart>().ToList();
    }

    public void Update()
    {

        for (int i = 0; i < BuildingParts.Count; i++)
        {
            if (!BuildingParts[i].isLost) {

                if (BuildingParts[i].isDamaged)
                {

                    BuildingParts[i].TickDamage();
                }

                if (BuildingParts[i].currHealth < 0)
                {
                    //destroy the building from the damage part to the top
                    for (int j = i; j < BuildingParts.Count; j++)
                    {
                        if(!BuildingParts[j].isLost)
                            BuildingParts[j].LosePart();                       
                    }
                }
            }

        }


    }



}

