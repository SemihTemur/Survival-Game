using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// e�er benim objemde Animator bile�eni eklentisi yoksa bu kod sayes�nde kend�s� otomat�kmen objeye bu bile�eni ekliyor.
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
        if (Input.GetMouseButtonDown(0)&&!EnvanterSistemiKontrolleri.Instance.acikMi&&!���ilikSistemiKontrolleri.Instance.a��kM�&&!Se�imY�neticisiKontrolleri.Instance.elG�r�n�yorsa)
        {
            animator.SetTrigger("hit");
        }
;  
    }

    // bu fonks�yonu an�masyon k�sm�n�n  30.san�yes�ne ekled�m ozaman bu cal�s�r
    public void Vur()
    {
        // mousun sol t�k�na her bast�g�mda model �al�s�cak ya model cal�st�g�nda benim ���n�m agaca dey�yorsa e�er bana o objeyi getir dedim;
        GameObject se�ilenAga� = Se�imY�neticisiKontrolleri.Instance.se�ilenA�ac;

        //  ���n  agaca  degmessede get�r�r  update k�sm�nda ya
        if (se�ilenAga� != null)
        {
            se�ilenAga�.GetComponent<A�acKesmeKontrolleri>().Sa�l�kAzals�n();
        }

    }




}
