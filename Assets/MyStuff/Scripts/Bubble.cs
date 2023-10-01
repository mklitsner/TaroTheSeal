using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
   
    [SerializeField]
        float speed = 0.1f;

    float lifeTime = 1;
    private bool hasTriggered;

    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

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
    }
}
