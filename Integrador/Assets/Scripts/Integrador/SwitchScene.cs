
using UnityEngine;

using UnityEngine.SceneManagement;


public class SwitchScene : MonoBehaviour
{

    public void Start()
    {
        
    }
    public void switchScenes( string Menu)
    {
        SceneManager.LoadScene(Menu);
    }

    public void RestartCurrentScene()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextScene()
    {
        
    }

}
