using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneScore : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerTwo"))
        {
            IncreaseScore();
            StartCoroutine(DisableCollider());
        }
    }

    void IncreaseScore()
    {
        score++;
        scoreText.text = "Player One Score: " + score;
    }

    IEnumerator DisableCollider()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }
}