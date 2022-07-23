using UnityEngine;
/// <summary>
/// PlayerMovement class
/// This class gives movement to the dps player
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;      //The character controller to move
    public Transform groundCheck;               //The gameObject to use as checker
    public LayerMask isGround;                  //Specifies the ground layer
    
    [Header("Move Settings")]
    public float speed = 10f;                   //Stores the movement speed of the player
    
    [Header("Jump Settings")]
    public bool isGrounded = false;             //Specifies if the player is grounded or not
    public float groundDistance = 0.2f;         //Specifies the minimum distance to reach the ground
    public float gravity = -9.81f;              //Stores the gravity value of the player
    public float jumpForce = 2.5f;              //Specifies how much the player can jump
    
    private Vector3 velocity;                   //Vector to calculate the player velocity

    /// <summary>
    /// Unity Start Callback
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    /// <summary>
    /// Unity Update Callback
    /// </summary>
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, isGround);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * z;

        ///Jump button input
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}