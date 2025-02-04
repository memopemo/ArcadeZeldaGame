using UnityEngine;
using UnityEngine.UI;

//keeps track of time in game
public class TimerUI : MonoBehaviour
{
    public int time; //easier to represent as a whole number instead of a float
    float decrementRate; //difficulty
    float waitToTallyTimer = 1; //delay in recognizing completion before tallying up timer
    CompletionChecker completionChecker;
    public static int[] timePerLevel = { 200, 300, 800, 800, 700, 600, 400, 800, 800, 1000 };
    // Start is called before the first frame update
    void Start()
    {
        time = timePerLevel[ScoreGame.level % 10];
        decrementRate = -0.001f * ScoreGame.level + 0.1f; //game becomes near impossible after level 70
        //decrementRate = 0.04f; //debug
        completionChecker = FindFirstObjectByType<CompletionChecker>();
        InvokeRepeating(nameof(DecrementTimer), 2, decrementRate);

        //set decrement rate based on level counter
    }

    // Update is called once per frame
    void Update()
    {
        //Update Timer
        if (completionChecker.enemiesLeft <= 0)
        {
            CancelInvoke(); //Cancel gameplay timer ticking

            //tally score
            if (waitToTallyTimer <= 0 && time > 0)
            {
                time -= 10;
                ScoreGame.score += 100;
                if (time < 0)
                {
                    time = 0;
                }
            }
            else
            {
                waitToTallyTimer -= Time.deltaTime;
            }
        }

        //build string for display, formatting with 0's and decimals as necessary.
        string textTime = time.ToString();
        if (textTime.Length == 2) //XX
        {
            textTime = textTime.Insert(0, "0");
        }
        textTime = textTime.Insert(textTime.Length - 1, "."); //fake decimal point

        if (time == 0) textTime = "00.0";
        GetComponent<Text>().text = textTime;

    }
    void DecrementTimer()
    {
        if (time == 0)
        {
            //Fail state
            FindFirstObjectByType<Player>().Die();
            CancelInvoke();
            return;
        }
        time--;
        if (time % 10 == 0)
        {
            GetComponent<AudioSource>().Play();
        }

    }
}
