using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPart : MonoBehaviour, IDamageable
{
    public float starterHealth = 10;
    public float currHealth;
    public bool isDamaged;
    public bool isLost;
    [SerializeField] MeshRenderer meshRendererDamaged;
    [SerializeField] MeshRenderer meshRendererHealthy;
    [SerializeField] GameObject Healthy;
    [SerializeField] GameObject Damaged;
    [SerializeField] FixTrigger[] fixTriggers;
    [SerializeField]  Collider col;
    [SerializeField] AudioClip damagedClip;
    [SerializeField] AudioClip DestroyClip;
    [SerializeField] AudioClip FixedClip;
    [SerializeField] AudioSource source;
    public Building building;
    public int stackNumber;

    private Color originalEmissionColor; // To store the original emission color
    public float maxFlickerFrequency = 100f; // Maximum flicker frequency when health is at its minimum


    private void Start()
    {
        originalEmissionColor= originalEmissionColor = meshRendererDamaged.material.GetColor("_EmissionColor");
        //Reset();
    }

   public void OnReset()
    {
        currHealth = starterHealth;
        meshRendererDamaged.material.color = Color.white;
        meshRendererHealthy.material.SetColor("Base Color", Color.white);
        Damaged.SetActive(false);
        Healthy.SetActive(true);
        col.enabled = true;
        foreach (var item in fixTriggers)
        {
            item.onReset();
        }
    }

    public void FixDamage(Action action)
    {
        bool isFixed = true;
        foreach (var item in fixTriggers)
        {
            if (!item.isFlownThrough)
            {
                isFixed = false;
            }
        }

        if (isFixed)
        {
            isDamaged = false;
            OnReset();
            UnityCoreHaptics.UnityCoreHapticsProxy.PlayContinuousHaptics(1, 1, .3f);
            source.PlayOneShot(FixedClip);
            building.FixPart(stackNumber);
            action.Invoke();
        }
       
    }

    public void TickDamage()
    {
        currHealth -= Time.deltaTime;
        Flicker();
    }

    public void TickInfected()
    {
        Color infectedColor = Color.Lerp(Color.black, Color.white, currHealth / starterHealth);
        meshRendererHealthy.material.SetColor("_BaseColor", infectedColor);
        meshRendererHealthy.material.SetColor("_Color", infectedColor);
    }

    private void Flicker()
    {

       
        float frequency = Mathf.Lerp(maxFlickerFrequency, 0.1f, currHealth / starterHealth);

        // Use Mathf.PingPong to oscillate between 0 and 1 based on frequency
        float lerpValue = Mathf.PingPong(Time.time * (0.5f+Mathf.Pow(frequency,2)), 1f);

        // Calculate the bright red color
        Color brightRed = Color.red * 2f;

        // Lerp between the original emission color and the bright red color based on the lerp value
        Color newEmissionColor = Color.Lerp(originalEmissionColor, brightRed, lerpValue);

        // Set the new emission color
        meshRendererDamaged.material.SetColor("_EmissionColor", newEmissionColor);
        meshRendererDamaged.material.EnableKeyword("_EMISSION");//This is a bug in unity
    }

    public void LosePart()
    {
        isLost = true;
        Healthy.SetActive(false);
        Damaged.SetActive(false);
        col.enabled = false;
        source.PlayOneShot(DestroyClip);
        this.enabled = false;

    }


    public void DoDamage()
    {

    }

    public void DoDamage(Vector3 hitPosition)
    {
        if (isDamaged) return;

        isDamaged = true;
        Debug.Log("Building Damaged");
        Damaged.SetActive(true);
        RotateObjectTowardsPosition(Damaged.transform, hitPosition);
        Healthy.SetActive(false);
        source.PlayOneShot(damagedClip);
    }

    void RotateObjectTowardsPosition(Transform objectToRotate, Vector3 targetPosition)
    {
        // Calculate the direction vector from the object to the target
        Vector3 directionToTarget = targetPosition - objectToRotate.position;

        // Set the y component of the direction vector to zero
        directionToTarget.y = -90;

        // Check if the direction is not zero
        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            // Calculate the rotation needed for the object to point towards the target along the Y-axis
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget.normalized);

            // Apply the rotation to the object's transform
            objectToRotate.rotation = targetRotation;
        }
    }
}
