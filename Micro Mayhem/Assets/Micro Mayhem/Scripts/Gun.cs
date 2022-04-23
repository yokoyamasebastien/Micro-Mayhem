using UnityEngine;

public class Gun : MonoBehaviour
{
    /*---------- Properties ----------*/
    [Header("Gun Attributes")]
    public int damage = 10;
    public float range = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(CameraMgr.inst.playerCam.transform.position, CameraMgr.inst.playerCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            /*----- Apply Damage to Enemy if hit -----*/
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
