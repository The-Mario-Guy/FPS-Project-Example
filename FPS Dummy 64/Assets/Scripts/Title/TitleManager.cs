using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject titleManager;
    public int sceneNumber;
    public Animator _startFade;
    public bool starting;
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(startGame());
        }
        _startFade.SetBool("starting", starting);
    }
    public void loadMain()
    {
        
    }
    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator startGame()
    {
        starting = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneNumber);
    }
}
