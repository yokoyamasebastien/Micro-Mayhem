using UnityEngine;

public class Gun : MonoBehaviour
{
    /*---------- Properties ----------*/
    [Header("Gun Attributes")]
    public int damage;

    public float timeBetweenShooting;
    public float spread;
    public float range;
    public float reloadTime;
    public float timeBetweenShots;

    public int magazineSize;
    public int bulletsPerTap;

    public bool allowButtonHold;

    public int bulletsLeft; 
    public int bulletsShot;

    [Header("Gun State")]
    public bool shooting;
    public bool readyToShoot;
    public bool reloading;

    [Header("Gun FX")]
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        // Set weapon state
        readyToShoot = false;

        // Calculate weapon spread
        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);

        Vector3 direction = CameraMgr.inst.playerCam.transform.forward + new Vector3(xSpread, ySpread, 0);

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(CameraMgr.inst.playerCam.transform.position, direction, out hit, range)){
            Debug.Log(hit.transform.name);

            /*----- Apply Damage to Enemy if hit -----*/
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if ((bulletsShot > 0) && (bulletsLeft > 0)){
            Invoke("Shoot", timeBetweenShots); 
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
