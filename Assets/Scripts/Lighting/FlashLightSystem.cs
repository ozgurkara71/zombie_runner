using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    // To fade light, intensity will be decreased by time
    [SerializeField] float lightDecay = .1f;
    [SerializeField] float angleDecay = 1f;
    // We don't want to light to become a point by shrinking. It should stop at certain angle
    [SerializeField] float minimumAngle = 40f;

    Light headLight;

    void Start()
    {
        headLight = GetComponent<Light>();
    }


    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    private void DecreaseLightAngle()
    {
        // Frame independence provided by Time.deltatime
        if (headLight.spotAngle <= minimumAngle) return;
        headLight.spotAngle -= angleDecay * Time.deltaTime;
    }

    private void DecreaseLightIntensity()
    {
        headLight.intensity -= lightDecay * Time.deltaTime;
    }

    public void RestoreLightAngle(float restoreAngle)
    {
        headLight.spotAngle = restoreAngle;
    }

    public void AddLightIntensity(float intensityAmount)
    {
        headLight.intensity += intensityAmount;
    }
}
