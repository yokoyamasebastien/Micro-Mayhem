using UnityEngine;

public class Gun : MonoBehaviour
{
    /*---------- Properties ----------*/
    [Header("Gun Attributes")]
    public int damage;

    public float force;

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

    public bool hitscan;   // If true, shoots bullets; if false, shoots projectiles

    public enum WeaponType { Pistol, Rifle, Sniper, Heavy, RocketLauncher };
    public WeaponType type;


    [Header("Gun State")]
    public bool shooting;
    public bool readyToShoot;
    public bool reloading;

    [Header("Gun FX")]
    public Transform attackPoint;
    public ParticleSystem muzzleFlash;
    //public GameObject bulletHole;

    [Header("Gun SFX")]
    public AudioClip gunshotSound;
    public AudioClip reloadSound;

    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If magazine is empty and not already reloading, reload weapon
        if((bulletsLeft <= 0) && !reloading) { Reload(); }
    }

    public void Shoot()
    {
        //Initially assigning spread so compiler doesnt yell at me. It gets reassigned below
        float xSpread = spread;
        float ySpread = spread;
        // Set weapon state
        readyToShoot = false;

        // Calculate sniper weapon spread if unscoped
        if (PlayerMgr.inst.currentGunID == 1)
        {
            if (!PlayerMgr.inst.isScoped)
            {
                xSpread = 3 * Random.Range(-spread, spread);
                ySpread = 3 * Random.Range(-spread, spread);
            }
        }
        else
        {   // Calculate default weapon spread
            xSpread = Random.Range(-spread, spread);
            ySpread = Random.Range(-spread, spread);
        }
        
        Vector3 direction = CameraMgr.inst.playerCam.transform.forward + new Vector3(xSpread, ySpread, 0);

        // Raycast
        RaycastHit hit;
        if (Physics.Raycast(CameraMgr.inst.playerCam.transform.position, direction, out hit, range)){
            
            /*----------Take care of rocket launcher projectile--------*/
            if (!hitscan)
            {
                Rigidbody clonedRocket;
                clonedRocket = Instantiate(GameMgr.inst.rocket, GameMgr.inst.rocketSpawnPoint.transform.position, GameMgr.inst.rocketSpawnPoint.transform.rotation);

                clonedRocket.velocity = transform.TransformDirection((Vector3.right - new Vector3(0, 0.5f, -.2f)) * 10f);
            }

            /*----- Apply Damage to Enemy if hit with bullet weapon-----*/
            else
            { 
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);   // Apply damage
                    enemy.enemyRB.AddForce(-hit.normal * force);    // Apply knockback force
                }
            }
        }

        //Play MuzzleFlash
        muzzleFlash.Play();

        // Play audio
        AudioMgr.inst.PlayGunFire();


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
        PlayerMgr.inst.weaponList[PlayerMgr.inst.currentGunID].SetActive(false);

        AudioMgr.inst.PlayGunReload();
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        PlayerMgr.inst.weaponList[PlayerMgr.inst.currentGunID].SetActive(true);
    }
}