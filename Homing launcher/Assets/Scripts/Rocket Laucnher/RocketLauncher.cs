using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private float RocketSpeed;
    [SerializeField] private float lockOnTimer = 1.5f;
    [SerializeField] private int AmmoCount = 1;
    [SerializeField] private int bulletsOnPlayer;
     
    [SerializeField] private GameObject Rocket;
    [SerializeField] private GameObject lockOnIcon;
    [SerializeField] private Transform BarrelHead;
    [SerializeField] private PlayerHud Hud;
    [SerializeField] private Camera cam;

    private bool startTimer;
    private bool LockedOn = false;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetMouseButtonDown(1) && LockedOn == false)
        {
            LockOn();
            startTimer = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (LockedOn == true)
            {
                LockOff();
            }
        }
        else if (lockOnTimer <= 0)
        {
          LockOff();
        }

        if (startTimer)
        {
            lockOnTimer -= Time.deltaTime;
        }
    }

    private void LockOff()
    {
        Destroy(GameObject.FindGameObjectWithTag("Target"));
        startTimer = false;
        LockedOn = false;
        lockOnTimer = 1.5f;
    }

    void LockOn()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200))
        {
            if (hit.transform.tag == "Enemy" && LockedOn == false)
            {
                GameObject icon = Instantiate(lockOnIcon, hit.transform.position, Quaternion.identity, hit.transform);
                LockedOn = true;
            }
        }
    }

    void Shoot()
    {
        if (AmmoCount != 0)
        {
            GameObject rocket = Instantiate(Rocket, BarrelHead.position, transform.rotation);
            if (LockedOn)
            {
                rocket.GetComponent<RocketHead>().smartRocket = true;
                rocket.GetComponent<Rigidbody>().velocity = (BarrelHead.forward * 20);
            }
            else
            {
                rocket.GetComponent<Rigidbody>().velocity = (BarrelHead.forward * RocketSpeed);
            }
            
            AmmoCount--;
            UpdateUI();
        }
    }

    void Reload()
    {
        if (AmmoCount == 0 && bulletsOnPlayer >= 1)
        {
            AmmoCount++;
            bulletsOnPlayer--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        Hud.UpdateAmmoCount(AmmoCount, bulletsOnPlayer);
    }
}
