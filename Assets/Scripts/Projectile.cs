using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Transform launchPoint;
    [SerializeField] private Transform targetTransform;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float v0;
            float time;
            float angle;
            float height;
            Vector3 groundDirectionNorm;

            ProjectileLibrary.CalculatePathFromLaunchToTarget(targetTransform.position, launchPoint.position, out groundDirectionNorm, out height, out v0, out time, out angle);
            StopAllCoroutines();
            StartCoroutine(ProjectileMovement(groundDirectionNorm, height, v0, angle, time));
        }
    }

    IEnumerator ProjectileMovement(Vector3 direction, float height, float v0, float angle, float time)
    {
        float t = 0;
        while(t < time)
        {
            transform.position = ProjectileLibrary.GetPositionAtTime(launchPoint.position, direction, v0, angle, t);
            Vector3 nextPosition = ProjectileLibrary.GetPositionAtTime(launchPoint.position, direction, v0, angle, t + Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(nextPosition - transform.position, Vector3.up);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
