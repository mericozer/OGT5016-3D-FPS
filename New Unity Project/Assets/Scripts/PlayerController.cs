using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private CharacterController charController;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float offset = -10f;

    private bool isGrounded = false;
    public bool isMapActive = false;

    private Vector3 velocity;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMapActive)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, offset, groundLayer); //checks if player is on the ground

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -1f;
            }
        
        
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 movementDirection = transform.right * x + transform.forward * z;

            charController.Move(movementDirection * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
        
            charController.Move(velocity * Time.deltaTime);
        }
        
    }
}
