using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    public Building[] Buildings { get; private set; }
    public MoveIgloo[] igloos;


    float bteweenShotTime = 3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Buildings == null) return;
        if (Buildings.Where(building => !building.isDestroyed).Count() == 0)
        {
            //GAMEOVER
        }
    }

    public void BeginGame()
    {
        Buildings = FindObjectsOfType<Building>();
        igloos = FindObjectsOfType<MoveIgloo>();

        StartCoroutine(InvokeRepeatedly());
    }

    IEnumerator InvokeRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(bteweenShotTime);
            ShootRandomPenguin();
        }
    }

    public void ShootRandomPenguin()
    {
        int i = Random.Range(0, igloos.Length);
        Building[] currBuildings = Buildings.Where(building => !building.isDestroyed).ToArray();
        int randBuilding = Random.Range(0, currBuildings.Length-1);
        BuildingPart[] currParts = currBuildings[randBuilding].BuildingParts.Where(part => !part.isLost && !part.isDamaged).ToArray();
        if (currParts.Length > 0)
        {
            int randPart = Random.Range(0, currParts.Length - 1);
            igloos[i].SetTargetAndShoot(currParts[randPart].transform);
        }
    }
}
