using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimateHandler : MonoBehaviour
{
    [SerializeField] public Animator animator2;
    [SerializeField] public Animator animator1;
    [SerializeField] public Animator animatorPCLight;
    [SerializeField] public GameObject animation1;
    [SerializeField] public GameObject animation2;

    [SerializeField] public GameObject animationPCLight;

    [SerializeField]
    private GameObject Blocker;
    public void Start()
    {
        animator2 = GetComponent<Animator>();
        animator1 = GetComponent<Animator>();
        //HideAllAnimation();
    }

    public void OnAnimationEnd()
    {
        animation1.SetActive(false);
        animation2.SetActive(true);
        

        if (animation2.activeSelf)
        {
            animator2.Play("windowsOS", 0, 0);
        }
        
        
    }

    public void TurnOFF()
    {
        animation1.SetActive(true);
        animator1.Play("osloading", 0, 0);
       
        animation2.SetActive(false);
        animationPCLight.SetActive(false);
        Blocker.SetActive(false);
    }
    public void OnAnimationEnd2()
    {
        
        animation2.SetActive(false);
        Blocker.SetActive(false);
        Status.text = "Tested";


    }
    public void HideAllAnimation()
    {
        animation1.SetActive(false);
        animation2.SetActive(false);
        animationPCLight.SetActive(false);
    }
    //public void ShowAllAnimation()
    //{
    //    if (!animation2.activeSelf && !animation1.activeSelf)
    //    {
    //        HideAllAnimation();
    //    }

    //    else if (animation2.activeSelf)
    //    {
    //        animation1.SetActive(true);
    //        animation2.SetActive(false);
    //    }
    //    else
    //    {
    //        animation1.SetActive(false);
    //        animation2.SetActive(true);
    //    }


    //}

    [SerializeField]
    private TMP_Text Status;
    public void OpenFirstAnimate()
    {
        animation1.SetActive(true);
        animationPCLight.SetActive(true);
        //animation2.SetActive(false);
        animator1.Play("osloading", 0, 0);
        animatorPCLight.Play("pclight2", 0, 0);

        if (Status.text != "Tested") {
            Status.text = "ON Testing...";
        }
        
    }
}
