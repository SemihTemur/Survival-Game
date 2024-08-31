using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemSilmeKontrolleri : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

// çopUyarýnýn içindeki text
    private Text degistirilecekMetin;


    private Image resimBileseni;

    Button evetBTN, hayirBTN;

    GameObject suruklenenNesne
    {
        get
        {
            return SürükleBýrak.sürüklenenÖðe;
        }
    }

    GameObject silinecekNesne;


    public string nesneAdi
    {
        get
        {
            string ad = silinecekNesne.name;
            string kaldirilacak = "(Clone)";
            string sonuc = ad.Replace(kaldirilacak, "");
            return sonuc;
        }
    }



    void Start()
    {
        resimBileseni = transform.Find("ArkaResim").GetComponent<Image>();

        degistirilecekMetin = ItemSilmeKontrolleriGUI.Instance.copUyariUI.transform.Find("Soru").GetComponent<Text>();

        evetBTN = ItemSilmeKontrolleriGUI.Instance.copUyariUI.transform.Find("Evet").GetComponent<Button>();
        evetBTN.onClick.AddListener(delegate { NesneyiSil(); });

        hayirBTN = ItemSilmeKontrolleriGUI.Instance.copUyariUI.transform.Find("Hayýr").GetComponent<Button>();
        hayirBTN.onClick.AddListener(delegate { SilmeIsleminiIptalEt(); });

    }

    // objemi çöp kutusuna býraktýgýmda OnDrop etkin hale gelir
    public void OnDrop(PointerEventData olayVerisi)
    {
        Debug.Log("OnDrop");
        silinecekNesne =SürükleBýrak.sürüklenenÖðe.gameObject;
        if (suruklenenNesne.GetComponent<ItemOzellikleriKontrolleri>().çöpAtýlabilir == true)
        {
      
         // burda suruklenenNesne'yý baska býr gameObjecte atmamýn nedený  suruklenennesne bundan sonra null olucak o yuzden olmadan once baska býr objeye atýp gameObjectý yok edýyorum bu sayede
            silinecekNesne = suruklenenNesne;

            StartCoroutine(silmedenOnceBildirim());
        }

    }

    IEnumerator silmedenOnceBildirim()
    {
        ItemSilmeKontrolleriGUI.Instance.copUyariUI.SetActive(true);
        degistirilecekMetin.text = "Bu " + nesneAdi + " atýlsýn mý?";
        yield return new WaitForSeconds(1f);
    }

    private void SilmeIsleminiIptalEt()
    {
        resimBileseni.sprite = ItemSilmeKontrolleriGUI.Instance.cop_kapali;
        ItemSilmeKontrolleriGUI.Instance.copUyariUI.SetActive(false);
    }

    private void NesneyiSil()
    {
        resimBileseni.sprite = ItemSilmeKontrolleriGUI.Instance.cop_kapali;
        DestroyImmediate(silinecekNesne);
        EnvanterSistemiKontrolleri.Instance.ÖðeListesiGüncelle();
        ÝþçilikSistemiKontrolleri.Instance.AraçOluþturmaKontrolu();
        ItemSilmeKontrolleriGUI.Instance.copUyariUI.SetActive(false);
    }

    // Envanterimdeki örneðin taþa mousumla týklýyken bu scriptin bulundugu objem yaný çop kutusunun uzerýne objemle(Örnek taþ) gelýrken bu kod çalýþýr
    public void OnPointerEnter(PointerEventData olayVerisi)
    {
    // null kontrolu yapmamýzýn nedený þu onDropa gýrmesse yaný çöple etkýlesýmde bulunmassa suruklebýrak classýndaký onEndDrag kýsmýndaký obje nulla eþit oldugu ýcýn
        if (suruklenenNesne != null && suruklenenNesne.GetComponent<ItemOzellikleriKontrolleri>().çöpAtýlabilir == true)
        {
            resimBileseni.sprite = ItemSilmeKontrolleriGUI.Instance.cop_acik;
        }

    }
    // Envanterimdeki örneðin taþa mousumla týklýyken bu scriptin bulundugu objem yaný çop kutusunun uzerýnden ayrýlýrken objemle(Örnek taþ)bu kod çalýþýr
    public void OnPointerExit(PointerEventData olayVerisi)
    {
        // null kontrolu yapmamýzýn nedený þu onDropa gýrmesse yaný çöple etkýlesýmde bulunmassa suruklebýrak classýndaký onEndDrag kýsmýndaký obje nulla eþit oldugu ýcýn
        if (suruklenenNesne != null && suruklenenNesne.GetComponent<ItemOzellikleriKontrolleri>().çöpAtýlabilir == true)
        {
            resimBileseni.sprite = ItemSilmeKontrolleriGUI.Instance.cop_kapali;
        }

    }

}
