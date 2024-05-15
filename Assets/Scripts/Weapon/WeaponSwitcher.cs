using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    int previousWeapon;

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        else if( Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(currentWeapon >= gameObject.transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentWeapon <= 0)
            {
                currentWeapon = gameObject.transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in gameObject.transform)
        {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
