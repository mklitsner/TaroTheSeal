using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SealControl : MonoBehaviour
{
    public UnityEvent onTap;
    public Animator animator;
    private bool canShoot;
    private float shootCooldownTime=0.2f;

    private Vector3 previousPosition;
    private Vector3 localVelocity;

    private void Start()
    {
        canShoot = true;
    }

    void Update()
    {

        var velocity = (transform.position - previousPosition) / Time.deltaTime;

        localVelocity = transform.InverseTransformDirection(velocity);

        previousPosition = transform.position;

        // Check if the screen is tapped or clicked
        if (Input.GetMouseButtonDown(0)&& canShoot) // 0 represents the left mouse button or a single touch tap
        {
            onTap.Invoke(); // Trigger the UnityEvent
            ShootCooldown();
            animator.SetTrigger("Shoot");
        }


        if (localVelocity.magnitude < 0.3f)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);

        }

        
        float turnValue = Map(localVelocity.x, -2, 2, 0, 1);


        animator.SetFloat("Turn", turnValue);

        Debug.Log("Angular Velocity: "+ localVelocity);
        Debug.Log("Velocity: " + localVelocity);

    }


    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldownTime);
        canShoot = true;
    }

    float Map(float value, float sourceMin, float sourceMax, float destMin, float destMax)
    {
        return (value - sourceMin) * (destMax - destMin) / (sourceMax - sourceMin) + destMin;
    }




}
