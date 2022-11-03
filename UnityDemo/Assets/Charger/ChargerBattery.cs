using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerBattery : MonoBehaviour
{
    [SerializeField] private ChargerType chargerType;
    public enum ChargerType
    {
        Main,
    }

    public ChargerType GetChargerType()
    {
        return chargerType;
    }
}
