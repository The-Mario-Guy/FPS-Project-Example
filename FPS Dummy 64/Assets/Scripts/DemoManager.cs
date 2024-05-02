using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoManager : MonoBehaviour
{
    public GameObject demo;

    public GameObject demoFader;
    public Animator _fade;
    public bool demoStart;
    public bool demoBegin;

    void Start()
    {
        demo.SetActive(false);
        _fade = demoFader.GetComponent<Animator>();

        demoFader.SetActive(false);
        StartCoroutine(DemoTimer());

    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKeyDown) 
        {
            
        }
      

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(demo.gameObject);
            demoFader.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            demoStart = true;
            demoBegin = true; //fades scene in to show demo
            demo.SetActive(true);
        }


        _fade.SetBool("demoStart", demoStart);
        _fade.SetBool("demoBegin", demoBegin);

    }

    IEnumerator DemoTimer()
    {
        yield return new WaitForSeconds(30f);//wait 30 secs for demo to play
        demoFader.SetActive(true); //fades scene out
        demoStart = true;
        yield return new WaitForSeconds(2f);
        demoBegin = true; //fades scene in to show demo
        demoStart = false;
        yield return new WaitForSeconds(0.1f);
        demoBegin = false;
        demo.SetActive(true); //starts demo video
        yield return new WaitForSeconds(67.2f);
        demo.SetActive(false);
        SceneManager.LoadScene(0);
    }

    
}
