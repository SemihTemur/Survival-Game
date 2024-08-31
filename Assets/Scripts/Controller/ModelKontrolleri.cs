using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// eðer benim objemde Animator bileþeni eklentisi yoksa bu kod sayesýnde kendýsý otomatýkmen objeye bu bileþeni ekliyor.
[RequireComponent(typeof(Animator))]
public class ModelKontrolleri : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

  
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!EnvanterSistemiKontrolleri.Instance.acikMi&&!ÝþçilikSistemiKontrolleri.Instance.açýkMý&&!SeçimYöneticisiKontrolleri.Instance.elGörünüyorsa)
        {
            animator.SetTrigger("hit");
        }
;  
    }

    // bu fonksýyonu anýmasyon kýsmýnýn  30.sanýyesýne ekledým ozaman bu calýsýr
    public void Vur()
    {
        // mousun sol týkýna her bastýgýmda model çalýsýcak ya model calýstýgýnda benim ýþýným agaca deyýyorsa eðer bana o objeyi getir dedim;
        GameObject seçilenAgaç = SeçimYöneticisiKontrolleri.Instance.seçilenAðac;

        //  ýþýn  agaca  degmessede getýrýr  update kýsmýnda ya
        if (seçilenAgaç != null)
        {
            seçilenAgaç.GetComponent<AðacKesmeKontrolleri>().SaðlýkAzalsýn();
        }

    }




}
