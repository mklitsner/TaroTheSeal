using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Building : MonoBehaviour
{
    public List<BuildingPart> BuildingParts;
    public bool isDestroyed;

    private void Start()
    {
        BuildingParts = GetComponentsInChildren<BuildingPart>().ToList();
        for (int i = 0; i < BuildingParts.Count; i++)
        {
            BuildingParts[i].stackNumber = i;
            BuildingParts[i].building = this;
        }
    }

    public void Update()
    {
        if (BuildingParts.Where(part => !part.isLost).Count() == 0)
        {
            isDestroyed = true;
        }

        if (isDestroyed) return;


        for (int i = 0; i < BuildingParts.Count; i++)
        {
            if (!BuildingParts[i].isLost) {

                if (BuildingParts[i].isDamaged)
                {

                    BuildingParts[i].TickDamage();

                    for (int j = i+1; j < BuildingParts.Count; j++)
                    {
                        if (!BuildingParts[j].isLost)
                        {
                            if (BuildingParts[j].isDamaged)
                            {
                                break;
                            }
                            BuildingParts[j].TickInfected();
                        }
                            
                    }
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

    public void FixPart(int stackNumber)
    {
        for (int j = stackNumber + 1; j < BuildingParts.Count; j++)
        {
            if (!BuildingParts[j].isLost)
            {
                if (BuildingParts[j].isDamaged)
                {
                    break;
                }
                BuildingParts[j].OnReset();
            }

        }
    }
}

