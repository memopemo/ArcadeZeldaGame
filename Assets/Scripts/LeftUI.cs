using UnityEngine;
using UnityEngine.UI;

//Displays Enemies left to kill on UI.
public class LeftUI : MonoBehaviour
{
    CompletionChecker completionChecker;
    void Start()
    {
        completionChecker = FindFirstObjectByType<CompletionChecker>();
    }
    void Update()
    {
        GetComponent<Text>().text = completionChecker.enemiesLeft.ToString();
    }
}
