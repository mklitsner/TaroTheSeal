using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public Transform launchPoint;
    public Vector3 forcePosition;
    public GameObject projectile;

    [Range(0f, 300f)]
    public float launchVelocity = 10f;

    [Range(0f, 10f)]
    public float angleStep = 5f;

    void Start()
    {
        StartCoroutine("FireProjectile");
    }

    void Update()
    {

        if(Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(0f, Input.GetAxis("Horizontal") * angleStep, 0f, Space.Self);
        }

        if(Input.GetAxis("Vertical") != 0)
        {
            transform.Rotate(0f, 0f, Input.GetAxis("Vertical") * angleStep, Space.Self);
        }
    }

    IEnumerator FireProjectile()
    {
        while(true) {
            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            _projectile.SetActive(true);
            // _projectile.GetComponent<Rigidbody>().velocity = -launchPoint.right * launchVelocity;
            _projectile.GetComponentInChildren<Rigidbody>().AddRelativeForce(-launchVelocity, 0f, 0f, ForceMode.Impulse);

            yield return new WaitForSeconds(2);
        }
    }
}
