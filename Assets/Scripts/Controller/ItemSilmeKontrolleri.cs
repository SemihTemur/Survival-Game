using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemSilmeKontrolleri : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

// �opUyar�n�n i�indeki text
    private Text degistirilecekMetin;


    private Image resimBileseni;

    Button evetBTN, hayirBTN;

    GameObject suruklenenNesne
    {
        get
        {
            return S�r�kleB�rak.s�r�klenen��e;
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

        hayirBTN = ItemSilmeKontrolleriGUI.Instance.copUyariUI.transform.Find("Hay�r").GetComponent<Button>();
        hayirBTN.onClick.AddListener(delegate { SilmeIsleminiIptalEt(); });

    }

    // objemi ��p kutusuna b�rakt�g�mda OnDrop etkin hale gelir
    public void OnDrop(PointerEventData olayVerisi)
    {
        Debug.Log("OnDrop");
        silinecekNesne =S�r�kleB�rak.s�r�klenen��e.gameObject;
        if (suruklenenNesne.GetComponent<ItemOzellikleriKontrolleri>().��pAt�labilir == true)
        {
      
         // burda suruklenenNesne'y� baska b�r gameObjecte atmam�n neden�  suruklenennesne bundan sonra null olucak o yuzden olmadan once baska b�r objeye at�p gameObject� yok ed�yorum bu sayede
            silinecekNesne = suruklenenNesne;

            StartCoroutine(silmedenOnceBildirim());
        }

    }

    IEnumerator silmedenOnceBildirim()
    {
        ItemSilmeKontrolleriGUI.Instance.copUyariUI.SetActive(true);
        degistirilecekMetin.text = "Bu " + nesneAdi + " at�ls�n m�?";
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
        EnvanterSistemiKontrolleri.Instance.��eListesiG�ncelle();
        ���ilikSistemiKontrolleri.Instance.Ara�Olu�turmaKontrolu();
        ItemSilmeKontrolleriGUI.Instance.copUyariUI.SetActive(false);
    }

    // Envanterimdeki �rne�in ta�a mousumla t�kl�yken bu scriptin bulundugu objem yan� �op kutusunun uzer�ne objemle(�rnek ta�) gel�rken bu kod �al���r
    public void OnPointerEnter(PointerEventData olayVerisi)
    {
    // null kontrolu yapmam�z�n neden� �u onDropa g�rmesse yan� ��ple etk�les�mde bulunmassa surukleb�rak class�ndak� onEndDrag k�sm�ndak� obje nulla e�it oldugu �c�n
        if (suruklenenNesne != null && suruklenenNesne.GetComponent<ItemOzellikleriKontrolleri>().��pAt�labilir == true)
        {
            resimBileseni.sprite = ItemSilmeKontrolleriGUI.Instance.cop_acik;
        }

    }
    // Envanterimdeki �rne�in ta�a mousumla t�kl�yken bu scriptin bulundugu objem yan� �op kutusunun uzer�nden ayr�l�rken objemle(�rnek ta�)bu kod �al���r
    public void OnPointerExit(PointerEventData olayVerisi)
    {
        // null kontrolu yapmam�z�n neden� �u onDropa g�rmesse yan� ��ple etk�les�mde bulunmassa surukleb�rak class�ndak� onEndDrag k�sm�ndak� obje nulla e�it oldugu �c�n
        if (suruklenenNesne != null && suruklenenNesne.GetComponent<ItemOzellikleriKontrolleri>().��pAt�labilir == true)
        {
            resimBileseni.sprite = ItemSilmeKontrolleriGUI.Instance.cop_kapali;
        }

    }

}
