using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class sObj_WeaponParams : ScriptableObject
{
    [SerializeField]
    private float maxShotSize;
    public float _maxShotSize => maxShotSize;

    [SerializeField]
    private float minShotSize;
    public float _minShotSize => minShotSize;

    [SerializeField]
    private float chargeSpeed;
    public float _chargeSpeed => chargeSpeed;

    [SerializeField]
    private float shotCoolDown;
    public float _shotCoolDown => shotCoolDown;

    [SerializeField]
    private float shotSpeed;
    public float _shotSpeed => shotSpeed;

    public GameObject playerShot;

}
