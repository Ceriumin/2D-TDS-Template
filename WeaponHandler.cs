using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header( "Statistics" )]
    public float cooldown = 0.75f;

    [Header( "Prefabs" )]
    //Drop in your equipped sprite in this prefab (top only)
    public GameObject equipped;
    public GameObject[] guns;

    private int currentWeaponIndex;
    //add all the keys corresponding the number of weapons you want.
    bool Key1, Key2 = false; 
    int totalWeapons = 1;
    [HideInInspector]
    public GameObject weaponHolder;
    [HideInInspector]
    public GameObject currentGun;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitching();
    }

    public void WeaponSwitching()
    {
        //Feel free to add more of the GetKey functions depending on the amount of weapons you want :)
        cooldown -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Alpha1) && Key1 == false)
        {
            if(cooldown < 0)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 0;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                equipped.SetActive(false);
                
                Key1 = true;
                Key2 = false;      
                cooldown = 0.75f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Key2 == false)
        {
            if(cooldown < 0)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                equipped.SetActive(true);

                Key2 = true;
                Key1 = false;
                cooldown = 0.75f; 
            }
        }
    }

    public void Initialization()
    {
        //Most of this is so the player spawns without anything equipped
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
        Key1 = true;
        equipped.SetActive(false);
    }
}


