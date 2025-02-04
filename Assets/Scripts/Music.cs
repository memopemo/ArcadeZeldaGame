using UnityEngine;

public class Music : MonoBehaviour
{
    void Start()
    {
        foreach (var music in FindObjectsOfType<Music>())
        {
            if (music == this) continue;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
