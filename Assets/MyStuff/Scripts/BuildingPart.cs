using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPart : MonoBehaviour, IDamageable
{
    public float starterHealth = 10;
    public float currHealth;
    public bool isDamaged;
    public bool isLost;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] GameObject Healthy;
    [SerializeField] GameObject Damaged;
    [SerializeField] FixTrigger[] fixTriggers;

    private void Start()
    {
        Reset();
    }

    void Reset()
    {
        currHealth = starterHealth;
        meshRenderer.material.color = Color.white;
        Damaged.SetActive(false);
        Healthy.SetActive(true);
        foreach (var item in fixTriggers)
        {
            item.onReset();
        }
    }

    public void FixDamage()
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
            Reset();
            UnityCoreHaptics.UnityCoreHapticsProxy.PlayContinuousHaptics(1, 1, .3f);
        }
       
    }

    public void TickDamage()
    {
        currHealth -= Time.deltaTime;
        meshRenderer.material.color = Color.Lerp(Color.red, Color.white, currHealth / starterHealth);
    }


    public void LosePart()
    {
        isLost = true;
        gameObject.SetActive(false);
    }


    public void DoDamage()
    {

    }

    public void DoDamage(Vector3 hitPosition)
    {
        isDamaged = true;
        Debug.Log("Building Damaged");
        Damaged.SetActive(true);
        RotateObjectTowardsPosition(Damaged.transform, hitPosition);
        Healthy.SetActive(false);
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
