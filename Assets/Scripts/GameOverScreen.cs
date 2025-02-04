using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Button
    public void Restart()
    {
        ScoreGame.score = 0;
        ScoreGame.level = 0;

        SceneManager.LoadScene("Lv0");
    }
}
