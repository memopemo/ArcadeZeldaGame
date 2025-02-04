using UnityEngine;
using UnityEngine.UI;

//Score display
public class ScoreUI : MonoBehaviour
{
    int counterScore = 0; //provides a "ticker" animation by slowly ticking up until we reach the desired score.
    void Start()
    {
        counterScore = ScoreGame.score; //prevent ticking all the way back up to our score every scene change.
        InvokeRepeating(nameof(PlayTickSound), 0.05f, 0.05f);
    }
    void Update()
    {
        if (ScoreGame.score > counterScore)
        {
            counterScore += 100; //max subdivision of points. (also makes it quicker)
        }
        GetComponent<Text>().text = counterScore.ToString();
    }
    void PlayTickSound()
    {
        if (counterScore != ScoreGame.score)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}

// Game-wide score keeping track
public static class ScoreGame
{
    public static int score;
    public static int level;
    public const int LEVELS_BEFORE_LOOPING = 10;
    public static int top; //high score
}
