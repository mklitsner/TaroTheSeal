using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject projectile;
    
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform targetTransform;

    void Start()
    {
        projectile.GetComponent<Projectile>().launchPoint = launchPoint;
        projectile.GetComponent<Projectile>().targetTransform = targetTransform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, launchPoint.position, Quaternion.identity);
        }
    }
}
