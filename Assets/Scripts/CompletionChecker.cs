using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletionChecker : MonoBehaviour
{
    public int enemiesLeft;
    public bool completed; //prevent from calling twice or more
    // Start is called before the first frame update
    void Awake()
    {
        ScoreGame.level = gameObject.scene.buildIndex - 1;
    }
    void Start()
    {
        completed = false;
        enemiesLeft += FindObjectsByType<Boss>(FindObjectsSortMode.None).Length;
        if (enemiesLeft != 0) return; //only enemy that matters is boss
        enemiesLeft += FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        ScoreGame.level = gameObject.scene.buildIndex - 1;

    }
    void Update()
    {
        if (enemiesLeft == 0 && !completed)
        {
            completed = true;
            StartCoroutine(nameof(WinScreenSteps));
        }
    }

    //Coroutines are helpful for spreading code across long lengths of time, eg "do this, then wait a couple seconds, then do this."
    IEnumerator WinScreenSteps()
    {
        // We have to find via script because we cant find via GameObject.Find() for inactive objects. 
        // Activating an object also restarts its animation
        FindFirstObjectByType<WinScreen>(FindObjectsInactive.Include).gameObject.SetActive(true);

        yield return new WaitForSeconds(3); //give enough time for points to be awarded

        GameObject.Find("Wipe").GetComponent<Animator>().Play("fadeout"); //wipe is always active and can be found.
        yield return new WaitForSeconds(0.5f); //length of fadeout's animation

        LoadNextLevel();
    }
    void LoadNextLevel()
    {
        ScoreGame.level += 1;
        //print(ScoreGame.level);
        int sceneLevel = ScoreGame.level % ScoreGame.LEVELS_BEFORE_LOOPING; //level increments forever, but scenes loop
        SceneManager.LoadScene("Lv" + sceneLevel);
    }
}
