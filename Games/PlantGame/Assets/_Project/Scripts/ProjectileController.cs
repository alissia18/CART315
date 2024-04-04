using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public const string KILLZONE_TAG = "KillZone";

    public const string PLANT_TAG = "Plant";
    
    private void OnTriggerEnter2D(Collider2D otherCol)
    {
        switch (otherCol.gameObject.tag)
        {
            case KILLZONE_TAG:
                Destroy(gameObject);
                break;
            case PLANT_TAG:
                PlantController plant = otherCol.gameObject.GetComponent<PlantController>();
                if (!plant.isAlive)
                {
                    plant.Revive();
                }
                Destroy(this.gameObject);
                break;
        }
    }
}
