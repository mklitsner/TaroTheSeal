using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Building[] Buildings { get; private set; }
    public MoveIgloo[] igloos;


    float bteweenShotTime = 3;

    int Wave = 0;
    bool inWave = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Buildings == null) return;


        if (inWave)
        {

        }
        else
        {

        }



        if (Buildings.Where(building => !building.isDestroyed).Count() == 0)
        {
            //GAMEOVER
            inWave = false;
        }
    }

    public void BeginGame()
    {
        Buildings = FindObjectsOfType<Building>();
        igloos = FindObjectsOfType<MoveIgloo>();
        IntoNextWave();
    }


    IEnumerator IntoNextWave()
    {
        Wave++;
        waveText.enabled = true;

        waveText.text = "Wave " + Wave;

        int num = 3;
        while (num > 0)
        {
            //setUItoNumber
            yield return new WaitForSeconds(1);
            num--;
        }
        waveText.enabled = false;
        StartWave();
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


    public void StartWave()
    {
        inWave = true;
        StartCoroutine(WaveCountDown(10+10*Wave));
    }


    public Text timerText; // Reference to the UI Text component
    public Text waveText;
    public Text amountText;
    public int timeInMinutes = 1; // Set your timer in minutes

    IEnumerator WaveCountDown(int amount)
    {
        caughtinWave = 0;
        CaughtText(amount);
        int totalTimeInSeconds = timeInMinutes * 60; // Convert minutes to seconds
        float shootInterval = (float)totalTimeInSeconds / amount; // Calculate the interval between function calls
        float nextShootTime = 0f; // Time for the next function call

        while (totalTimeInSeconds >= 0)
        {
            // Calculate minutes and seconds from total seconds
            int minutes = totalTimeInSeconds / 60;
            int seconds = totalTimeInSeconds % 60;
            CaughtText(amount);

            // Update the UI Text component
            timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

            // Check if it's time to shoot a penguin
            if (totalTimeInSeconds <= nextShootTime)
            {
                ShootRandomPenguin();
                nextShootTime += shootInterval; // Schedule the next function call
            }

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrement the total time
            totalTimeInSeconds--;
        }
        inWave = false;
        
        StartCoroutine(IntoNextWave());
    }

    private void CaughtText(int amount)
    {
        amountText.text = caughtinWave + "/" + amount;
    }

    int caughtinWave;

    public void CaughtPenguin()
    {
        caughtinWave++;
    }
}
