using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = ScoreGame.level.ToString();
    }
}
