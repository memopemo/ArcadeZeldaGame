using UnityEngine;
using UnityEngine.UI;


public class HighScoreUI : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = ScoreGame.top.ToString();
    }
}
