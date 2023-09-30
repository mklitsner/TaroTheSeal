using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] Transform ShootLocation;

    public void ShootBubble()
    {
        Instantiate(bubblePrefab, ShootLocation.position, ShootLocation.rotation);
    }

}
