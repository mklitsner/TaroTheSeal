using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public Transform launchPoint;
    [SerializeField] public Transform targetTransform;

    [SerializeField] private GameObject missile;
    [SerializeField] private ParticleSystem smoke;
    public float speed=0.3f;
    private bool hasTriggered;

    public AudioSource source;
    public AudioClip hum;
    public AudioClip caught;
    Vector3 launchPos;

    public void Launch(Transform _targetTransform, Transform _launchPoint)
    {
        targetTransform = _targetTransform;
        launchPoint = _launchPoint;
        launchPos = launchPoint.position;
        float v0;
        float time;
        float angle;
        float height;
        Vector3 groundDirectionNorm;

        ProjectileLibrary.CalculatePathFromLaunchToTarget(targetTransform.position, launchPos, out groundDirectionNorm, out height, out v0, out time, out angle);
        StopAllCoroutines();
        StartCoroutine(ProjectileMovement(groundDirectionNorm, height, v0, angle, time));
    }

    IEnumerator ProjectileMovement(Vector3 direction, float height, float v0, float angle, float time)
    {
        float t = 0;
        while(t < time)
        {
            transform.position = ProjectileLibrary.GetPositionAtTime(launchPos, direction, v0, angle, t);
            Vector3 nextPosition = ProjectileLibrary.GetPositionAtTime(launchPos, direction, v0, angle, t + Time.deltaTime* speed);
            transform.rotation = Quaternion.LookRotation(nextPosition - transform.position, Vector3.up);
            t += Time.deltaTime* speed;
            yield return null;
        }
        Destroy(missile);
        smoke.Stop();
        Destroy(gameObject, 10f);
    }


    public void OnCaught()
    {
        source.PlayOneShot(caught);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Hittable"))
        {
            hasTriggered = true;

            IDamageable damageable;
            if (other.TryGetComponent(out damageable))
            {
                damageable.DoDamage(transform.position);
                Debug.Log("PenguinHit");
            }

            Destroy(gameObject);
        }
    }
}
