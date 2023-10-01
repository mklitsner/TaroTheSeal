using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    public Building[] Buildings { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Buildings = FindObjectsOfType<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Buildings.Where(building => !building.isDestroyed).Count() == 0)
        {
            //GAMEOVER
        }
    }
}
