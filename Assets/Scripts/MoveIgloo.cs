using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIgloo : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform targetTransform; 

    void Update()
    {
        Vector3 direction = ProjectileLibrary.GetDirection(targetTransform.position, launchPoint.position);
        float height = ProjectileLibrary.GetHeight(targetTransform.position, launchPoint.position);

        Vector3 highestPointInProjectile = new Vector3(direction.x, height, direction.z);
        transform.rotation = Quaternion.LookRotation(highestPointInProjectile, Vector3.up);
    }
}
