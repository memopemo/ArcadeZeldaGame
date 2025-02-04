using UnityEngine;

public class Switch : MonoBehaviour
{
    bool activated;
    Color startColor;
    void Start()
    {
        startColor = GetComponent<MeshRenderer>().material.color;
    }
    void Update()
    {
        if (activated)
        {
            transform.Rotate(new Vector3(Time.deltaTime * 180, Time.deltaTime * 180, Time.deltaTime * 180));
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = startColor;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Slash _))
        {
            if (!activated)
            {
                FindFirstObjectByType<Boss>().SwitchActivated();
                activated = true;
            }
        }
    }
    public void Deactivate()
    {
        activated = false;
    }
}
