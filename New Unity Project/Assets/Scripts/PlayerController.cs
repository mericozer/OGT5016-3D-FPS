using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private CharacterController charController;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;

    private Animator anim;
    [SerializeField] private Animator shotGunAnim;
    [SerializeField] private Animator autoGunAnim;
 
    private float speed;
    [SerializeField] private float normalSpeed = 10f;
    [SerializeField] private float runSpeed = 20f;
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
        speed = normalSpeed;
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
            
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = normalSpeed;
            }
        
        
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x != 0 || z != 0)
            {
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }

            Vector3 movementDirection = transform.right * x + transform.forward * z;

            charController.Move(movementDirection * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
        
            charController.Move(velocity * Time.deltaTime);
        }
        
    }

    public void ChangeGun(int index)
    {
        if (index == 0)
        {
            anim = shotGunAnim;
        }
        else
        {
            anim = autoGunAnim;
        }
    }
    public void Reload(bool reload)
    {
        anim.SetBool("Reload", reload);
    }
}
