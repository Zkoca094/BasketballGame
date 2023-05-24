using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{    
    public GameObject gameoverPanel;
    public GameObject gameCamera;
    private void Awake()
    {
        gameoverPanel.SetActive(false);
        gameCamera.SetActive(false);
    }
    public void GameOverRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            AudioManager.instance.Play("Failed");
            Manager.singleton.gameState = State.Failed;
            other.GetComponent<BallMovement>().enabled = false;
            gameoverPanel.SetActive(true);
            gameCamera.SetActive(true);
        }
    }
}
