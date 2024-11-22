using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkLoadMenu();
    }

    public void loadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("StartingScene");
    }

    public void checkLoadMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
