using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// eğer benim objemde Animator bileşeni eklentisi yoksa bu kod sayesınde kendısı otomatıkmen objeye bu bileşeni ekliyor.
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
        if (Input.GetMouseButtonDown(0)&&!EnvanterSistemiKontrolleri.Instance.acikMi&&!İşçilikSistemiKontrolleri.Instance.açıkMı&&!SeçimYöneticisiKontrolleri.Instance.elGörünüyorsa)
        {
            animator.SetTrigger("hit");
        }
;  
    }

    // bu fonksıyonu anımasyon kısmının  30.sanıyesıne ekledım ozaman bu calısır
    public void Vur()
    {
        // mousun sol tıkına her bastıgımda model çalısıcak ya model calıstıgında benim ışınım agaca deyıyorsa eğer bana o objeyi getir dedim;
        GameObject seçilenAgaç = SeçimYöneticisiKontrolleri.Instance.seçilenAğac;

        //  ışın  agaca  degmessede getırır  update kısmında ya
        if (seçilenAgaç != null)
        {
            seçilenAgaç.GetComponent<AğacKesmeKontrolleri>().SağlıkAzalsın();
        }

    }




}
