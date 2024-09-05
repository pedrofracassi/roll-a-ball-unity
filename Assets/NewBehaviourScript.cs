using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float speed;
    public Rigidbody rb;

    float maxSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // jump
    void Update()
    {
        // jump only if the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * 2.0f, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // jump only if the player is on the ground
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(movement);
        }

        // if out of bounds, reset the player
        if (rb.position.y < -1.0f)
        {
            rb.position = new Vector3(0.0f, 0.5f, 0.0f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            // destroy
            Destroy(other.gameObject);
        }
    }
}
