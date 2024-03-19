using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Add a public Text field for the winning message
    public Text winningMessageText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckScore(int score, string playerTag)
    {
        if (score >= 4)
        {
            EndGame(playerTag);
        }
    }

    private void EndGame(string winningPlayerTag)
    {
        Debug.Log(winningPlayerTag + " wins!");

        // Display the winning message
        winningMessageText.text = winningPlayerTag + " wins!";
        winningMessageText.enabled = true;

        // Start the coroutine to delay the restart
        StartCoroutine(DelayedRestart());
    }

    private IEnumerator DelayedRestart()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}