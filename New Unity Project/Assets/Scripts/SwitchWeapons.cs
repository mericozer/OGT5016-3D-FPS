using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    private int weaponIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWeaponIndex = weaponIndex;
        
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)  //change the weapon index
        {
            if (weaponIndex >= transform.childCount - 1)
            {
                weaponIndex = transform.childCount - 1;
            }
            else
            {
                weaponIndex++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponIndex <= 0)
            {
                weaponIndex = 0;
            }
            else
            {
                weaponIndex--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) //gun 1
        {
            weaponIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount > 1) //gun 2 if there is one
        {
            weaponIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount > 2) //gun 3 if there is one
        {
            weaponIndex = 3;
        }

        if (currentWeaponIndex != weaponIndex) //if weapon index is not the same with current weapon, make the current weapon the same with the weapon index
        {
            SelectWeapon();
            CanvasController.instance.ChangeGun(); //change gun sprite
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        
        foreach(Transform weapon in transform)
        {
            if (weaponIndex == i)
            {
                weapon.gameObject.SetActive(true);
                PlayerController.instance.ChangeGun(weaponIndex);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
