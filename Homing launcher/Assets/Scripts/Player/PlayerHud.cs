using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] TMP_Text AmmoCount;

    public void UpdateAmmoCount(int ammoCount, int bulletsOnPlayer)
    {
       AmmoCount.text = "Ammo: " + ammoCount + "/" + bulletsOnPlayer.ToString();
    }
}
