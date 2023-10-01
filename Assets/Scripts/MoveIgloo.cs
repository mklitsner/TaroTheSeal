using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIgloo : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
private Transform targetTransform;
    [SerializeField] GameObject projectilePrefab;

    void Update()
    {
        if (targetTransform == null) return;
        Vector3 direction = ProjectileLibrary.GetDirection(targetTransform.position, launchPoint.position);
        float height = ProjectileLibrary.GetHeight(targetTransform.position, launchPoint.position);

        Vector3 highestPointInProjectile = new Vector3(direction.x, height, direction.z);
        transform.rotation = Quaternion.LookRotation(highestPointInProjectile, Vector3.up);
    }


    public void SetTargetAndShoot(Transform target)
    {
        targetTransform = target;
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Launch(target, launchPoint);
    }
}
