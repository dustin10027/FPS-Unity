using UnityEngine;
/// <summary>
/// Autodestroys this gameobject when an amount of seconds has passed.
/// </summary>
public class SelfDestruct : MonoBehaviour
{
    /// <summary>
    /// Unity Update Callback
    /// </summary>
    void Start()
    {
        Destroy(gameObject, 15f);
    }
}