using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public Building[] Buildings { get; private set; }
    public MoveIgloo[] igloos;
    public AudioLooper looper;


    float bteweenShotTime = 3;

    int Wave = 0;
    bool inWave = false;
    bool startScreen=true;
    public GameObject[] Onboarding;


    // Start is called before the first frame update
    void Start()
    {
        StartGameContainer.SetActive(true);
        StatsContainer.SetActive(false);
        waveContainer.SetActive(false);
        foreach (var item in Onboarding)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (startScreen)
        {
            if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button or a single touch tap
            {
                StartGameContainer.SetActive(false);
                Onboarding[0].SetActive(true);
                startScreen = false;
            }
        }




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
            GameOverContainer.SetActive(true);
        }
    }

    public void BeginGame()
    {
        startScreen = false;
        StartGameContainer.SetActive(false);
        StartGameContainer.SetActive(false);
        Onboarding[0].SetActive(true);
        Buildings = FindObjectsOfType<Building>();
        igloos = FindObjectsOfType<MoveIgloo>();
        looper.isStart = true;
        Onboarding[0].SetActive(false);
        StatsContainer.SetActive(true);
        StartCoroutine(IntoNextWave());
    }


    IEnumerator IntoNextWave()
    {
        Wave++;
        waveContainer.SetActive(true);
        looper.countDownAudio.Play();
        waveText.text = Wave.ToString();
        int countdown = 3; // Start countdown from 3
        while (countdown > -1)
        {
            countdownText.text = countdown.ToString(); // Display current countdown value
            yield return new WaitForSeconds(1f); // Wait for one second
            countdown--; // Decrease the countdown
        }
        waveContainer.SetActive(false);
        StartWave(); // Call StartWave function when countdown reaches 0
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
            igloos[i].rotateIgloo.CallRotate();
        }
    }


    public void StartWave()
    {
        inWave = true;
        StartCoroutine(WaveCountDown(10+5*Wave));
    }


    public TMP_Text timerText; // Reference to the UI Text component
    public TMP_Text waveText;
    public GameObject waveContainer;
    public TMP_Text countdownText;
    public TMP_Text caughtText;
    public TMP_Text amountText;
    public GameObject GameOverContainer;
    public GameObject StartGameContainer;
    public GameObject StatsContainer;
    public int timeInMinutes = 1; // Set your timer in minutes

    IEnumerator WaveCountDown(int amount)
    {
        caughtinWave = 0;
        CaughtText(amount);
        int totalTimeInSeconds = timeInMinutes * 60; // Convert minutes to seconds
        float shootInterval = (float)totalTimeInSeconds / amount; // Calculate the interval between function calls
        float nextShootTime = totalTimeInSeconds; // Time for the next function call
       
        while (totalTimeInSeconds >= 0)
        {
            // Calculate minutes and seconds from total seconds
            int minutes = totalTimeInSeconds / 60;
            int seconds = totalTimeInSeconds % 60;
            CaughtText(amount);

            // Update the UI Text component
            timerText.text = string.Format("{0:D1}:{1:D2}", minutes, seconds);
            // Check if it's time to shoot a penguin
            if (totalTimeInSeconds <= nextShootTime)
            {
                ShootRandomPenguin();
                nextShootTime -= shootInterval; // Schedule the next function call
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
        amountText.text = amount.ToString();
        caughtText.text = caughtinWave.ToString();
    }

    int caughtinWave;

    public void CaughtPenguin()
    {
        caughtinWave++;
    }
}
