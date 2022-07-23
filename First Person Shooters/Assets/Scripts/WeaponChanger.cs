using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Allows the player to change between 2 different weapons
/// </summary>
public class WeaponChanger : MonoBehaviour
{
    [Header("References")]
    public GameObject weapon1;
    public GameObject weapon2;
    public Text ammoText;

    [Header("Settings")]
    public bool isChanging = false;             //Bool flag to control if the player is changing the weapon
    public float timeToChangeWeapon = 0.2f;     //The time to change between weapons
    private float timer = 0f;                   //Timer to control the weapon change countdown

    /// <summary>
    /// Unity Update Callback
    /// </summary>
    void Update()
    {
        if (isChanging)
        {
            if (timer < timeToChangeWeapon)
            {
                timer += Time.deltaTime;
            }
            else
            {
                isChanging = false;
                timer = 0f;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0  
            && weapon1.GetComponent<Weapon>().isReloading == false
            && weapon2.GetComponent<Weapon>().isReloading == false
            && isChanging == false)
        {
            if (weapon1.activeInHierarchy == true)
            {
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            }
            else
            {
                weapon1.SetActive(true);
                weapon2.SetActive(false);
            }
            isChanging = true;
        }

        if (weapon1.activeInHierarchy == true)
        {
            ammoText.text = weapon1.GetComponent<Weapon>().currentAmmo.ToString();
        }
        else {
            ammoText.text = weapon2.GetComponent<Weapon>().currentAmmo.ToString();
        }
    }
}