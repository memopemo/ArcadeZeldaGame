using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject attack;
    void OnTriggerStay(Collider c)
    {
        if (c.TryGetComponent(out Slash s))
        {
            Instantiate(attack, transform.position, Quaternion.identity);
            Invoke(nameof(LoadGame), 1);
        }
    }
    void LoadGame()
    {
        SceneManager.LoadScene("Lv0");
    }
}
