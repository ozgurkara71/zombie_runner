using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float intensityAmount = 5;
    [SerializeField] float increaseAngle = 71f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInChildren<FlashLightSystem>().RestoreLightAngle(increaseAngle);
            other.gameObject.GetComponentInChildren<FlashLightSystem>().AddLightIntensity(intensityAmount);
            Destroy(gameObject);
        }
    }
}
