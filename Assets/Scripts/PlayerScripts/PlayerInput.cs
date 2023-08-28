using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float speed;
    public float leftRightTurnSpeed;
    public static float leftside = -4.40f;
    public static float rightside = 4.40f;
    public float internalRight;
    public float internalLeft;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        internalLeft = leftside;
        internalRight = rightside;
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);



        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > leftside)
            {
                transform.Translate(Vector3.left * leftRightTurnSpeed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < rightside)
            {
                transform.Translate(Vector3.right * leftRightTurnSpeed * Time.deltaTime);
            }
        }
    }
}
