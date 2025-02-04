using UnityEngine;

//walks back and forth.
public class StrollerEnemy : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(Flip), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.forward * speed * Time.deltaTime;
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
    }
    void Flip()
    {
        transform.Rotate(Vector3.up, 180);
    }
}
