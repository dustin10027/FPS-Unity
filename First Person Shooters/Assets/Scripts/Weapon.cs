using UnityEngine;
/// <summary>
/// Weapon Main class. 
/// Performs a shoot in the game.
/// </summary>
public class Weapon : MonoBehaviour
{
    [Header("References")]
    public Camera cam; 
    public GameObject bulletDecal; 
    public WeaponChanger weaponChanger;
    public Animator anim;

    [Header("Shoot Values")]
    public float range = 100f;              //The distance of the raycast
    public float shootRate = 15f;           //Specifies how fast the gun should do a shot
    private float nextTimeToShoot = 0f;     //Timer to calculate the next shot
    public bool autoShoot = false;          //Specifies if the gun is automatic
    public int currentAmmo;                 //Stores the current ammunition of the weapon
    public int ammoCapacity = 30;           //Specifies the ammunition capacity of the weapon
    public bool isReloading = false;        //Boolean flag to control if the weapon is reloading
    public float reloadDuration = 1f;       //Specifies the duration of a reload
    private float reloadTimer = 0f;         //Timer to control the reload duration

    [Header("Sound")]
    public AudioSource audioS;
    public AudioClip shootClip;
    public AudioClip reloadClip;

    /// <summary>
    /// Unity Start Callback
    /// </summary>
    void Start() 
    {
        currentAmmo = ammoCapacity;
    }
    /// <summary>
    /// Unity update callback.
    /// </summary>
    void Update()
    {
        if (isReloading)
        {
            if (reloadTimer < reloadDuration)
            {
                reloadTimer = reloadTimer + Time.deltaTime;
            }
            else {
                currentAmmo = ammoCapacity;
                isReloading = false;
                reloadTimer = 0f;
            }
        }
        else {
            if (currentAmmo > 0 && weaponChanger.isChanging == false) {
                ListenShootInput();
            }

            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < ammoCapacity) {
                isReloading = true;
                anim.SetTrigger("Reload");
                audioS.PlayOneShot(reloadClip);
            }
        }
    }
    /// <summary>
    /// Listens the keyboard input from the 
    /// </summary>
    private void ListenShootInput() 
    {
        if (autoShoot)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
            {
                nextTimeToShoot = Time.time + 1f / shootRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }
    /// <summary>
    /// Performs a Raycast to see if a bullet hits a gameObject.
    /// </summary>
    private void Shoot() 
    {
        ///Calling the animator
        anim.SetTrigger("Shoot");

        ///Sound Trigger
        audioS.PlayOneShot(shootClip);

        ///Raycast Logic
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range)) 
        {
            GameObject decal = Instantiate(bulletDecal, hit.point + (hit.normal * 0.025f), Quaternion.identity) as GameObject;
            decal.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            decal.transform.parent = hit.transform;
            
            if (hit.transform.gameObject.tag == "Target") 
            {
                Destroy(hit.transform.gameObject);
                FindObjectOfType<GameManager>().remainingTargets--;
            }
        }
        currentAmmo--;
    }
}
