using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum PointsGiven { k1, k2, k5, k8 } //prevents OOB bugs
    public PointsGiven pointsGiven;
    [SerializeField] GameObject[] pointParticles;
    [SerializeField] GameObject explode;
    [SerializeField] AudioClip die;
    int[] points = { 1000, 2000, 5000, 8000 };
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Slash _))
        {
            GameObject birthedSound = new GameObject();
            birthedSound.AddComponent<AudioSource>().clip = die;
            birthedSound.GetComponent<AudioSource>().PlayDelayed(0.05f);
            //die
            //create point and death particles
            Instantiate(explode, transform.position, Quaternion.identity);
            Instantiate(pointParticles[(int)pointsGiven], transform.position, Quaternion.identity);

            //update score and goal
            ScoreGame.score += points[(int)pointsGiven];
            //RIP
            Destroy(gameObject);

            if (FindFirstObjectByType<Boss>()) return; //dont decrement if boss is in room
            FindFirstObjectByType<CompletionChecker>().enemiesLeft--;
        }
    }
}
