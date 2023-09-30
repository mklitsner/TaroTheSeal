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


    private void Start()
    {
        Reset();
    }

    void Reset()
    {
        currHealth = starterHealth;
        meshRenderer.material.color = Color.white;
    }

    public void FixDamage()
    {
        isDamaged = false;
        Reset();
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

    }
}
