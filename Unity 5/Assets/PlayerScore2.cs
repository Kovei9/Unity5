using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
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
        if (other.gameObject.CompareTag("PlayerOne"))
        {
            IncreaseScore();
            StartCoroutine(DisableCollider());
        }
    }

    void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    IEnumerator DisableCollider()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }
}