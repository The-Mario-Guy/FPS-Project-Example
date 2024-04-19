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
    public bool titleCard;
    public int titleCardSecs;
    public GameObject credits;
    public GameObject instructions;
    public AudioSource start;
    void Start()
    {
        start = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !titleCard)
        {
            start.Play(1);
            StartCoroutine(startGame());
        }
        if (Input.GetKeyDown(KeyCode.C) && !titleCard)
        {
            credits.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.V) && !titleCard)
        {
            credits.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.I) && !titleCard)
        {
            instructions.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.V) && !titleCard)
        {
            instructions.SetActive(false);
        }
        _startFade.SetBool("starting", starting);

        if (titleCard == true)
        {
            StartCoroutine(titleCardThingy());
        }
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
        yield return new WaitForSeconds(0.5f);
        starting = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneNumber);
    }

    IEnumerator titleCardThingy()
    {
        yield return new WaitForSeconds(titleCardSecs);
        SceneManager.LoadScene(sceneNumber);
    }
}
