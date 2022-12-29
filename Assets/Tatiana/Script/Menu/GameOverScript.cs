using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void Quit()
    {
        Debug.Log("Quitter");
        Application.Quit();
    }

   

    public void Continue()
    {
        Debug.Log("Continue");
        SceneManager.LoadScene("Level1");
    }
}
