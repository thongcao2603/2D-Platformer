using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    float timeToFire = 0;

    public float Damage = 10;
    public LayerMask whatToHit;


    float timeToSpawn = 0;
    public float effectSpawnRate = 10;
    Transform firePoint;
    public Transform bulletTrailPrefab;
    public Transform muzzlePrefab;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = transform.Find("FirePoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }


    void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 firePosition = firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(firePosition, mousePosition - firePosition, 100, whatToHit);
        if (Time.time >= timeToSpawn)
        {
            Effect(mousePosition);
            timeToSpawn = Time.time + 1 / effectSpawnRate;
        }
        if (hit.collider != null)
        {
            Debug.Log(hit.collider);
        }
    }

    void Effect(Vector2 mousePos)
    {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation).GetComponent<LineRenderer>();
        Transform muzzle = Instantiate(muzzlePrefab, firePoint.position, firePoint.rotation) as Transform;
        muzzle.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        muzzle.localScale = new Vector3(size, size, size);
        Destroy(muzzle.gameObject, 0.02f);
    }
}
