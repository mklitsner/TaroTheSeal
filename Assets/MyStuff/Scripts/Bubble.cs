using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
   
    [SerializeField]
        float speed = 0.1f;

    [SerializeField]
    float upSpeed = 0.1f;

    float lifeTime = 1;
    private bool hasTriggered;
    bool hasCaughtEnemy;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCaughtEnemy)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime, Space.World);
        }
        


        if (lifeTime > 0)
        {
            

            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

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
                Debug.Log("Damaged");
            }

            Destroy(gameObject);
        }
        else if(other.CompareTag("Enemy"))
        {
            other.transform.parent = transform;
            other.transform.localPosition = Vector3.zero;
            other.GetComponent<Projectile>().StopAllCoroutines();
            GetComponent<Collider>().enabled = false;
            hasCaughtEnemy = true;
            FindObjectOfType<GameplayManager>().CaughtPenguin();
        }
    }
}
