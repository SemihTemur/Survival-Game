using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// bu scrıptın oldugu objelere  collider ekle dıyor yoksa  tabi
[RequireComponent(typeof(BoxCollider))]
public class AğacKesmeKontrolleri : MonoBehaviour
{
    // ağac kesmek için agaca dogru gıttıgımızda agaca yaklastıgımızda agacı keselım dıye var
    public bool oyuncuAlandaMı;
    public bool kesilebilirMi;

    public float agaçMaxSağlık;
    public float ağaçSağlık;

    private Animator animator;

    private void Start()
    {
        ağaçSağlık = agaçMaxSağlık;
        // burda anımasyonumuz en usttekı parenta oldugu ıcın bu sekılde yapıp ordan aldık anımasyonu
        animator = transform.parent.transform.parent.GetComponent<Animator>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandaMı = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandaMı = false;
        }
    }

    public void SağlıkAzalsın()
    {
        animator.SetTrigger("Shake");

        ağaçSağlık -= 1;

        if (ağaçSağlık <= 0)
        {
            AğaçÖldü();
        }

    }


    public void AğaçÖldü()
    {
        // objeyı yok etmeden once konumunu saklıyoruz.
        Vector3 ağacınSonKonumu = transform.position;
        Destroy(transform.parent.transform.parent.gameObject);
        kesilebilirMi = false;
        SeçimYöneticisiKontrolleri.Instance.seçilenAğac = null;
        SeçimYöneticisiKontrolleriGUI.Instance.ağacBilgileri.gameObject.SetActive(false);

        GameObject kesilenAğac = Instantiate(Resources.Load<GameObject>("KesilmişAğac"), new Vector3(ağacınSonKonumu.x,ağacınSonKonumu.y+3,ağacınSonKonumu.z), Quaternion.Euler(0, 0, 0));   

    }


    private void Update()
    {
        // burası dogruysa gırsın
        if (kesilebilirMi)
        {
            AğaçSilmeKontrolleriGUI.Instance.slider.value = ağaçSağlık / agaçMaxSağlık;
        }
    }

}
