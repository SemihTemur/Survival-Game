using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;
public class ItemOzellikleriKontrolleri : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    // --- Bu öğe çöpe atılabilir mi --- //
    public bool çöpAtılabilir;

    // --- Öğe Bilgisi UI --- //
    private GameObject bilgiEkranıUI;

    private Text öğeBilgiUI_öğeAdı;
    private Text öğeBilgiUI_öğeAçıklama;
    private Text öğeBilgiUI_öğeFonksiyon;

    public string buAd, buAçıklama, buFonksiyon;

    // --- Tüketilebilirlik --- //
    private GameObject tüketimBekleyenÖğe;
    public bool tüketilebilir;

    public int sağlıkEtkisi;

    // --- HızlıSlotaEklenebilir objeler --- //
    public bool EkiplenebilirMi;
    private GameObject ekiplemeBekleyenÖğe;
    public bool hızlıSlotunİçindeMi;
    
    // Eğer bu aktif olursa yanı hızlıslotta seçilmisse bu obje onun envantere suruklenmesını ıstemedıgımız ıcın yanı suruklenmesını ıstemedıgımız ıcın bu degıskenı olusuturp hızlıslot kısmında kontrol edıyoruz eger bu true olursa suruklebırak scrıptı aktıf olmuyor.yanı suruklenemıyor.
    public bool seçildiMi;

    private void Start()
    {
        bilgiEkranıUI = EnvanterSistemiKontrolleriGUI.Instance.envanterÖğeBilgiEkranıUı;
        öğeBilgiUI_öğeAdı = bilgiEkranıUI.transform.Find("İtemİsmi").GetComponent<Text>();
        öğeBilgiUI_öğeAçıklama = bilgiEkranıUI.transform.Find("İtemTanımı").GetComponent<Text>();
        öğeBilgiUI_öğeFonksiyon = bilgiEkranıUI.transform.Find("İtemınİşlevi").GetComponent<Text>();
        bilgiEkranıUI.gameObject.SetActive(false);
    }

    void Update()
    {
        // bu şu demek ornegın ben hızlı slotumda 1.cı kısmı seçtim ya o seçtiğim objenın Envantere sürüklenmesini istemiyorum bu sayede o surukleme ıslemını yapan kısmı ınaktıf hale getırıyorum.
        if (seçildiMi == true)
        {
            gameObject.GetComponent<SürükleBırak>().enabled = false;
        }
        // başka slotu seçersek burası eskı halını donsun ıstıyorum
        else
        {
            gameObject.GetComponent<SürükleBırak>().enabled = true;
        }
    }

    // Fare öğenin bu betiğe sahip olduğu alanına girdiğinde tetiklenir.
    public void OnPointerEnter(PointerEventData olayVerisi)
    {
        if (gameObject.transform.parent.CompareTag("Slot"))
        {
            bilgiEkranıUI.SetActive(true);
            öğeBilgiUI_öğeAdı.text = buAd;
            öğeBilgiUI_öğeAçıklama.text = buAçıklama;
            öğeBilgiUI_öğeFonksiyon.text = buFonksiyon;
        }
        
    }


    // Fare öğenin bu betiğe sahip olduğu alanı terk ettiğinde tetiklenir.
    public void OnPointerExit(PointerEventData olayVerisi)
    {
        bilgiEkranıUI.SetActive(false);
    }


    //Bu olay, bir UI elemanına fare tıklandığında veya dokunmatik etkileşimde tetiklenir. önce bu çalısır
    public void OnPointerDown(PointerEventData olayVerisi)
    {
        Debug.Log(" OnPointerDown");
        // Sağ Tıklama
        if (olayVerisi.button == PointerEventData.InputButton.Right)
        {
            // ornegın baltanın tuketilebilirligi false olur cunku balta tuketılmez Inspector penceresınden halledıcem o kısımları ornegın baltanın false olucak
           if (tüketilebilir)
           {
               bilgiEkranıUI.SetActive(false);
                //gameObjectı baska bır degıskenı atamamızın nedenı  OnPointerUp fonksıyonun ıcerısınde yapmıs oldugumuz kontrolden dolayıdır eger o kontrolu yapmasak bız baska gameObjectlere tıklamasak bıle onlarda yok olur
               tüketimBekleyenÖğe = gameObject;
               SaglikEtkisiHesaplama(sağlıkEtkisi);
           }

            // yanı burda ornegın balta ekıplenebılır yanı ısıme yarar agac kesmede falan o yuzden Inspector penceresınde baltanın Ekıplenebılırlıgını true yapıcam sonra hızlıslot kısmına bakıcam  dolu degılse suruklebırak yaptıgımda hızlıslot kısmına gozukucek baltam ama bır tane olmasını ıstıyorum hızlıslot kısmında o yuzden ŞuanEkıplendı dıye bır bool'um var ekıplenırse false olucak bır tane daha balta olursa gırmıcek buraya
            else if (EkiplenebilirMi==true && hızlıSlotunİçindeMi == false && HızlıSlotKontrolleri.Instance.DoluMu() == false)
            {
                HızlıSlotKontrolleri.Instance.HızlıYuvayaEkle(gameObject);
                hızlıSlotunİçindeMi = true;
            }

        }

    


   }

    // Fare sağ tıkına bastıktan sonra elını cekınce bu tetıklenır. sonra bu çalışır
    public void OnPointerUp(PointerEventData olayVerisi)
    {
        Debug.Log("OnPointerUp");
        if (olayVerisi.button == PointerEventData.InputButton.Right)
        {
            if (tüketilebilir && tüketimBekleyenÖğe == gameObject)
            {
                //Destroydan farkı anında objeyı yok eder destroy ıse bir sonrakı frame erteler.
                DestroyImmediate(gameObject);
                EnvanterSistemiKontrolleri.Instance.ÖğeListesiGüncelle(); 
                İşçilikSistemiKontrolleri.Instance.AraçOluşturmaKontrolu();
            }
        }
    }

    private  void SaglikEtkisiHesaplama(int saglikEtkisii)
    {
        // --- Sağlık --- //

        int oncekiSaglik = SağlıkKontrolleri.Instance.getSağlık();
        int maksimumSaglik =100;

        if (saglikEtkisii != 0)
        {
            if ((oncekiSaglik + saglikEtkisii) > maksimumSaglik)
            {
                SağlıkKontrolleri.Instance.setSağlık(maksimumSaglik);
            }
            else
            {
                saglikEtkisii += oncekiSaglik;
                SağlıkKontrolleri.Instance.setSağlık(saglikEtkisii);
            }
        }
    }

}


