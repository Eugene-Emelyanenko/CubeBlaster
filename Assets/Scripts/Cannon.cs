using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Text ammoText;
    [SerializeField] private Transform line;
    [SerializeField] private WaveManager waveManager;
    public int maxAmmo = 10;
    public int startAmmo = 1;
    public GameObject projectilePrefab;
    public float shootInterval = 1f;
    public Transform shootPoint;

    private int collectedAmmo;
    private int ammo;
    static public bool canShoot;

    private void Start()
    {
        collectedAmmo = 0;
        ReloadCannon();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.y > line.position.y)
            {
                canShoot = false;
                Vector2 direction = (mousePos - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        while (ammo > 0)
        {
            GameObject ball = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            ammo--;
            ammoText.text = ammo.ToString();
            
            yield return new WaitForSeconds(shootInterval);
        }
    }

    public void End()
    {
        waveManager.NextWave();
        ReloadCannon();
        collectedAmmo = 0;
    }

    public void BonusAmmo()
    {
        collectedAmmo++;
    }

    private void ReloadCannon()
    {
        startAmmo += collectedAmmo;
        if (startAmmo > maxAmmo)
            startAmmo = maxAmmo;
        ammo = startAmmo;
        canShoot = true;
        ammoText.text = ammo.ToString();
    }
}
