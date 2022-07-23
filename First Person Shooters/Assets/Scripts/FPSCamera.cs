using UnityEngine;
/// <summary>
/// First Person Shooter Camera Class.
/// </summary>
public class FPSCamera : MonoBehaviour
{
    [Header("References")]
    public Transform player;            //A Reference to the player
    [Header("Settings")]    
    public float lookSensivity = 100f;  //Specifies the speed of the rotation
    private float verticalRotation = 0f;

    /// <summary>
    /// Unity Start Callback
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    /// <summary>
    /// Unity Update Callback
    /// </summary>
    void Update()
    {
        ///Listen the input of the mouse
        float x = Input.GetAxis("Mouse X") * lookSensivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * lookSensivity * Time.deltaTime;

        verticalRotation -= y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        player.Rotate(Vector3.up * x);
    }
}