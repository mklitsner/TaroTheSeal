using UnityEngine;
using System.Collections;
using System;

public class Building : MonoBehaviour, IDamageable
{
    [SerializeField] int starterHealth=10;
    [SerializeField] int currHealth;


    private void Start()
    {
        currHealth = starterHealth;
    }


    public void DoDamage(int amount)
    {
        currHealth -= amount;

        if (currHealth < 0) currHealth = 0;

        if (currHealth == 0)
        {
            Console.WriteLine("building is destroyed");
            Destroy(this);
        }
        else
        {
            Console.WriteLine($"building damaged, {currHealth} health remaining");
        }
    }
}

