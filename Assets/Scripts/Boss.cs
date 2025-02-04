using UnityEngine;

public class Boss : MonoBehaviour
{
    int health = 9;
    public int swtichesNotActivated = 4;
    [SerializeField] AudioClip die;
    [SerializeField] GameObject explode;

    void OnTriggerEnter(Collider other)
    {
        //cannot be hit if shields are up
        if (swtichesNotActivated > 0) return;

        if (other.TryGetComponent(out Slash _))
        {
            health -= 1;
            if (health == 0)
            {
                //Die
                ScoreGame.score += 10000;
                FindFirstObjectByType<CompletionChecker>().enemiesLeft--;
                Destroy(gameObject);
            }
            if (health % 3 == 0) //after being hit 3 times
            {
                // Regenerate shield
                transform.GetChild(0).gameObject.SetActive(true);

                //reactivate switches
                swtichesNotActivated = 4;
                foreach (var swtch in FindObjectsByType<Switch>(FindObjectsSortMode.None))
                {
                    swtch.Deactivate();
                }
            }

            //Hit Sound
            GameObject birthedSound = new GameObject();
            birthedSound.AddComponent<AudioSource>().clip = die;
            birthedSound.GetComponent<AudioSource>().Play();

            //Hit Particle
            GameObject particle = Instantiate(explode, transform.position, Quaternion.identity);
            particle.transform.localScale = transform.localScale;

        }
    }
    public void SwitchActivated()
    {
        swtichesNotActivated -= 1;
        if (swtichesNotActivated == 0)
        {
            //visually disable shield
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
