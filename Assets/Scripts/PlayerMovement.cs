using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.00001f, 10.0f)]
    public float speed;
    public Rigidbody rb;

    float maxSpeed = 15.0f;

    private Vector2 moveInputValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*
        // jump
        void Update()
        {
            // jump only if the player is on the ground
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                rb.AddForce(Vector3.up * 2.0f, ForceMode.Impulse);
            }
        }
        */

    private void OnJump(InputValue value)
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * 2.0f, ForceMode.Impulse);

        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        Vector3 movement = new Vector3(input.x * speed, 0.0f, input.y * speed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(movement);
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

            // play sound
            AudioSource audio = GameObject.Find("JumpSound").GetComponent<AudioSource>();
            audio.Play();
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

            AudioSource audio = GameObject.Find("PickupSound").GetComponent<AudioSource>();
            audio.Play();

            // add score
            ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
            scoreKeeper.AddScore(1);
        }

        if (other.gameObject.CompareTag("TimePickup"))
        {
            // destroy
            Destroy(other.gameObject);

            // play sound
            AudioSource audio = GameObject.Find("ExtraTimeSound").GetComponent<AudioSource>();
            audio.Play();

            // add score
            TimeKeeper timeKeeper = GameObject.Find("Timer").GetComponent<TimeKeeper>();
            timeKeeper.AddTime(7);
        }

        if (other.gameObject.CompareTag("BombPickup"))
        {
            // play sound
            AudioSource audio = GameObject.Find("HitSound").GetComponent<AudioSource>();
            audio.Play();

            // destroy
            Destroy(other.gameObject);

            // remove health
            HealthKeeper healthKeeper = GameObject.Find("Health").GetComponent<HealthKeeper>();
            healthKeeper.RemoveHealth(1);
        }
    }
}
