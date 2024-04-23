using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKeyDown) 
        {
            StartCoroutine(DemoTimer());
        }
        else
        {
            StartCoroutine(demoStop());
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(demo.gameObject);
            demoFader.SetActive(false);
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
        yield return new WaitForSeconds(66f);
        demoBegin = false;
        demo.SetActive(false);
        demoStart = true;
        yield return new WaitForSeconds(1f);
        demoBegin = true;
        yield return new WaitForSeconds(0.5f);

    }

    IEnumerator demoStop()
    {
        StopCoroutine(DemoTimer());
        yield return new WaitForSeconds(5f);
        StartCoroutine(DemoTimer());
    }
}
