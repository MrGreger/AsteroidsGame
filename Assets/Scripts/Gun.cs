using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    public void Shoot()
    {
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
        Debug.Log("Shoot!");
    }
}
