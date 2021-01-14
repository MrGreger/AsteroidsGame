using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent Shooted;

    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeReference]
    private SpaceShip _shooter;

    private void Start()
    {
        if(_shooter == null)
        {
            Debug.LogError("Gun can not be without shooter!");
        }
    }

    public void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
        bullet.SetOwner(_shooter);

        Shooted?.Invoke();
    }
}
