using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeçimYöneticisiKontrolleri: MonoBehaviour
{
    public static SeçimYöneticisiKontrolleri Instance {get; private set;}//burda design patternlerden singleton ýlkesý var
    private Text interaction_text;
    public  bool hedef;
    public GameObject etkileþimliObje;
    // el görunuyorsa beným baltamýn anýmasyonu calýsmasýn cunku baltamýn anýmasyonu sol týka basýnca calýsýyor ama ayný zamanda envantere býsey eklýyýcegýmýz zamanda sol týka basýyoruz onun kontrolu bu. bunu ModelKontrolleri scrýptýnde cagýrýcam
    public bool elGörünüyorsa;
    public GameObject seçilenAðac;
    
    
    private void Start()
    {
        hedef = false;
        interaction_text = SeçimYöneticisiKontrolleriGUI.Instance.interaction_Info_UI.GetComponent<Text>();// içindeki yazýya eriþmemizi saðlýyor bu kod sadece yazýya eriþebiliyoruz.
        
        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
          
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// Ekran üzerindeký noktanýn koordýnatlarýný býze verýr
        RaycastHit hit;

        //Envanterý actýgýmýzda ýþýn gondermeyý býraksýn dýye bunu yaptýk !EnvanterSistemi.Instance.acikMi  cunku envanter acýkken býle yerdeký tasý falan alýyor
        if (Physics.Raycast(ray, out hit)&&!EnvanterSistemiKontrolleri.Instance.acikMi)//nesne varsa buraya gýrer ama gokyuzu býr nesne degýl o yuzden else kýsmýný ekledým cunku else kýsmý olmasaydý býr nesneye denk geldýkten sonra nesne olmayan býr seye denk geldýgýnde yazý kapanmýyordu
        {
            var selectionTransform = hit.transform;
           //referans alýyoruz burda bu sayede EtkileþimliObjenýn ýcerýsýndeký degerlere erýsebýlýyoruz.
            EtkileþimliObje seçilenObje = selectionTransform.GetComponent<EtkileþimliObje>();

            AðacKesmeKontrolleri aðacKesme = selectionTransform.GetComponent<AðacKesmeKontrolleri>();
            // gönderdiðim aðac ýþýna geliyorsa ve oyuncuda alandaysa
            //Aðaçlar için
            if (aðacKesme && aðacKesme.oyuncuAlandaMý)
            {
                aðacKesme.kesilebilirMi = true;
                seçilenAðac = aðacKesme.gameObject;
                SeçimYöneticisiKontrolleriGUI.Instance.aðacBilgileri.gameObject.SetActive(true);
            }

            //Eðer oyuncu Alanda deðilse ama ýþýn agaca deyýyorsa
            else
            {
                // yaný ornegin;ben ilk baþta alana girdiðimde seçýlenAgac null olmiyacak ama sonra alandan cýkarsam null olmasýný ýstedýgým ýcýn bunu eklýyorum
                if (seçilenAðac != null)
                {
                    seçilenAðac.GetComponent<AðacKesmeKontrolleri>().kesilebilirMi = false;
                    seçilenAðac = null;
                    SeçimYöneticisiKontrolleriGUI.Instance.aðacBilgileri.gameObject.SetActive(false);
                }
                else
                {
                    seçilenAðac = null;
                    SeçimYöneticisiKontrolleriGUI.Instance.aðacBilgileri.gameObject.SetActive(false);
                }
            }

            // eklenebilenler ýcýn
            if (seçilenObje &&seçilenObje.gameObject.CompareTag("Eklenebilir") && seçilenObje.oyuncuAlandamý)
            {

     // bu hedef boolen degýskený tum scrýptlerde true oldugu ýcýn dýger scrýptlerde oyuncualandamý true olursa dýger hepsýde raycast ustlerýne gelmeden yok olur bunun ýcýn raycaste gelen kýsmýn gameObjecýný alýp onu raycaste gelen kýsýmdaký objenýn scrýptýyle esýtlememýz lazým.
                hedef = true;
                elGörünüyorsa = true;
                etkileþimliObje = seçilenObje.transform.gameObject;
                interaction_text.text = seçilenObje.GetItemName();

                SeçimYöneticisiKontrolleriGUI.Instance.interaction_Info_UI.gameObject.SetActive(true);
                SeçimYöneticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(false);
                SeçimYöneticisiKontrolleriGUI.Instance.el.gameObject.SetActive(true);
            }

            else
            {
                hedef = false;
                elGörünüyorsa = false;
                SeçimYöneticisiKontrolleriGUI.Instance.interaction_Info_UI.gameObject.SetActive(false);
                SeçimYöneticisiKontrolleriGUI.Instance.el.gameObject.SetActive(false);
                SeçimYöneticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(true);
            }

        }

        else // nesne yoksa buraya gýrer
        {
            seçilenAðac = null;
            SeçimYöneticisiKontrolleriGUI.Instance.aðacBilgileri.gameObject.SetActive(false);
            hedef = false;
            elGörünüyorsa = false;
            seçilenAðac = null;
            SeçimYöneticisiKontrolleriGUI.Instance.interaction_Info_UI.gameObject.SetActive(false);
            SeçimYöneticisiKontrolleriGUI.Instance.el.gameObject.SetActive(false);
            if(EnvanterSistemiKontrolleri.Instance.acikMi || ÝþçilikSistemiKontrolleri.Instance.açýkMý)
            {
                SeçimYöneticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(false);
            }
            else
            {
                SeçimYöneticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(true);
            }
           
        }

    }





}
