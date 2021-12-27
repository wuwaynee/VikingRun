using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    bool alive = true;
    Animator animator;
    public float speed = 5;
    [SerializeField] Rigidbody rb;
    
    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    public bool isGrounded = true;

    private void Start()
    {
        animator = player.GetComponent<Animator>();
            
    }
    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 fowardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * 9 * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + fowardMove + horizontalMove);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Quaternion angle = Quaternion.Euler(0, -90, 0);
            transform.rotation = transform.rotation * angle;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Quaternion angle = Quaternion.Euler(0, 90, 0);
            transform.rotation = transform.rotation * angle;
        }

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }    
    }


    public void Die()
    {
        animator.SetBool("death", true);
        alive = false;
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        // Check whether we are currently grounded
        float height = GetComponent<Collider>().bounds.size.y;
        //bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        // If we are, jump
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }
}
