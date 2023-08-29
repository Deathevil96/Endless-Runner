using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public float leftRightTurnSpeed;
    public static float leftside = -4.40f;
    public static float rightside = 4.40f;
    public float internalRight;
    public float internalLeft;
    private int _maxHealth = 2;
    private int _currHealth;

    //Touch variables
    private Vector2 touchStartPos;

    // Start is called before the first frame update
    void Start()
    {
        _currHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()

    {
        internalLeft = leftside;
        internalRight = rightside;
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);


        // Handle touch swipe controls
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 touchEndPos = touch.position;
                    Vector2 swipeDelta = touchEndPos - touchStartPos;

                    // Check if the swipe is horizontally significant
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.x > 0)
                        {
                            MoveRight();
                        }
                        else if (swipeDelta.x < 0)
                        {
                            moveLeft();
                        }
                    }
                    break;
            }
        }


        //Keyboard Controls
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft();
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    void moveLeft()
    {
        if (transform.position.x > leftside)
        {
            transform.Translate(Vector3.left * leftRightTurnSpeed * Time.deltaTime);
            Debug.Log("Touch working left");
        }
    }
    void MoveRight()
    {
        if (transform.position.x < rightside)
        {
            transform.Translate(Vector3.right * leftRightTurnSpeed * Time.deltaTime);
            Debug.Log("Touch working Right");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }

        if (other.CompareTag("SoftObstacle"))
        {
            _currHealth -= 1;
            Debug.Log("CareFul!");
            if (_currHealth <= 0)
            {
                Die();
            }
        }
    }
    public void takeDamage(int Damage)
    {
        _currHealth -= Damage;

        if (_currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
