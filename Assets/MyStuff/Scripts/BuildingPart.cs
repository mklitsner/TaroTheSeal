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
        isDamaged = true;
        Debug.Log("Building Damaged");
        Damaged.SetActive(true);
        Healthy.SetActive(false);
    }
}
