using UnityEngine;

// Lights slowly turn red as the timer nears 0
public class Lights : MonoBehaviour
{
    TimerUI timerUI;
    Light light;
    Color startColor;
    CompletionChecker completionChecker;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        timerUI = FindFirstObjectByType<TimerUI>();
        completionChecker = FindFirstObjectByType<CompletionChecker>();
        startColor = light.color;
    }

    // Update is called once per frame
    void Update()
    {
        //this is to prevent the lights from turing red when the timer awards points.
        if (completionChecker.enemiesLeft > 0)
        {
            //slowly turn the lights more evil as the timer gets closer to 0.
            light.color = Color.Lerp(startColor, Color.red, 1 - (timerUI.time / (float)TimerUI.timePerLevel[ScoreGame.level % ScoreGame.LEVELS_BEFORE_LOOPING]));
        }
    }
}
