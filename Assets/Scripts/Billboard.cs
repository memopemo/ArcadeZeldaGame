using UnityEngine;

//intro billboards that face the camera and do nothing else.
public class Billboard : MonoBehaviour
{
    Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindFirstObjectByType<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera);
        transform.Rotate(0, 180, 0);
    }
}
