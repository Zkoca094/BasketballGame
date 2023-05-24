using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animation anim;
    public GameObject ball;
    public float speed = 10f;
    public void PlayBtn()
    {
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        if (!anim.IsPlaying("MainMenuAnimation"))
        {
            ball.transform.Rotate(Vector3.up, Time.deltaTime * speed);
        }
    }
}
