using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string sceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {

    }
}
