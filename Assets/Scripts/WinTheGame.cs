using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinTheGame : MonoBehaviour
{
    [SerializeField] private GameObject confetiEffectPrefab;
    [SerializeField] private GameObject gameWinPanel;

    private void Awake()
    {
        gameWinPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ball")
        {
            AudioManager.instance.Play("Win");
            Manager.singleton.gameState = State.Win;
            GameObject confetiEffect = Instantiate(confetiEffectPrefab, transform.position, Quaternion.identity);
            confetiEffect.GetComponent<ParticleSystem>().Play();
            Destroy(confetiEffect, 2f);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            new WaitForSeconds(2f);
            gameWinPanel.SetActive(true);
        }
    }
}
