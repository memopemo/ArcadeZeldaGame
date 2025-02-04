using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float force = 20;
    float baseFloatLevel;
    // Start is called before the first frame update
    void Start()
    {
        baseFloatLevel = transform.position.y;
        InvokeRepeating(nameof(Flap), 0, 0.05f);
    }
    void Flap()
    {
        //flap only if we are below our original height to maintain the same y position
        if (transform.position.y < baseFloatLevel)
        {
            GetComponent<Rigidbody>().velocity += Vector3.up * force;
        }
    }
}
