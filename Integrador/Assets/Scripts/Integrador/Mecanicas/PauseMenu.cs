using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public Button pauseButton;
    public Button resumeButton;

    private void Start()
    {
      pauseButton.onClick.AddListener(OnButtonClick);
        
      resumeButton.onClick.AddListener(disableMenu);
        
        pauseMenu.SetActive(false);
    }

    private void Update()
    {

        
      
    }

    void OnButtonClick()
    {
        pauseMenu.SetActive(true);       
    }

    void disableMenu()
    {
        pauseMenu.SetActive(false);
    }

}
