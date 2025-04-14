using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void RetryBtn()
    {
        SceneManager.LoadScene("MainScene");
    }
}
