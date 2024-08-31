using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnvanterSistemiKontrolleri : MonoBehaviour
{
    // Sýnglenton var burda
    public static EnvanterSistemiKontrolleri Instance { get; set; }

    public List<GameObject> slotListesi = new List<GameObject>();

    public List<string> ögeListesi = new List<string>();

    private GameObject eklenecekÖge;

    private GameObject hangislotaEklensin;

    public bool acikMi;


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

    void Start()
    {
        acikMi = false;
  

        ToplamSlotListesi();
    }

    private void ToplamSlotListesi()
    {
        // unýtyde býr objenýn cocuklarýný transform ýbaresýyle alýyorsun
        foreach (Transform cocuk in EnvanterSistemiKontrolleriGUI.Instance.envanterEkranUI.transform)
        {
            if (cocuk.CompareTag("Slot"))
            {
                slotListesi.Add(cocuk.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !acikMi)
        {
            EnvanterSistemiKontrolleriGUI.Instance.envanterEkranUI.SetActive(true);
            acikMi = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.I) && acikMi)
        {
            EnvanterSistemiKontrolleriGUI.Instance.envanterEkranUI.SetActive(false);

            //Eðer Ýþçilik ekraný açýksa mouse ekranda gözüksün diye bunu yapýyoruz.
            if (!ÝþçilikSistemiKontrolleri.Instance.açýkMý)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            acikMi = false;
        }
    }


    public void EnvantereEkle(string itemName)
    {
        hangislotaEklensin = SonrakiBoþSlotBul();
        eklenecekÖge = Instantiate(Resources.Load<GameObject>(itemName), hangislotaEklensin.transform.position, hangislotaEklensin.transform.rotation);
        eklenecekÖge.transform.SetParent(hangislotaEklensin.transform);

        ögeListesi.Add(itemName);

        StartCoroutine(BilgiEkranýnýGöster(itemName, eklenecekÖge.GetComponent<Image>().sprite));

        ÖðeListesiGüncelle();
        ÝþçilikSistemiKontrolleri.Instance.AraçOluþturmaKontrolu();
    }


    public IEnumerator BilgiEkranýnýGöster(string name, Sprite sprite)
    {
        EnvanterSistemiKontrolleriGUI.Instance.bilgiEkraný.gameObject.SetActive(true);
        EnvanterSistemiKontrolleriGUI.Instance.ekleneninÝsmi.text = name;
        EnvanterSistemiKontrolleriGUI.Instance.ekleneninResmi.sprite = sprite;
        yield return new WaitForSeconds(5);
        EnvanterSistemiKontrolleriGUI.Instance.bilgiEkraný.gameObject.SetActive(false);
    }



    private GameObject SonrakiBoþSlotBul()
    {

        foreach (GameObject slot in slotListesi)
        {

            if (slot.transform.childCount == 0)
            {
                return slot;
            }

        }

        return new GameObject();
    }


    public bool DolumuKontrolu()
    {
        int counter = 0;

        foreach (GameObject slot in slotListesi)
        {

            if (slot.transform.childCount > 0)
            {
                counter++;
            }

        }

        return counter == 21;
    }


    public void EnvanterdenKaldýr(string kaldýrýlýcakItem, int kaldýrýlýcakItemSayýsý)
    {
        int sayaç = kaldýrýlýcakItemSayýsý;
        for (int i = slotListesi.Count - 1; i >= 0; i--)
        {

            if (slotListesi[i].transform.childCount > 0)
            {

                if (slotListesi[i].transform.GetChild(0).name == kaldýrýlýcakItem + "(Clone)" && sayaç != 0)
                {

                    DestroyImmediate(slotListesi[i].transform.GetChild(0).gameObject);
                    sayaç--;
                }

            }

        }

        ÖðeListesiGüncelle();
        ÝþçilikSistemiKontrolleri.Instance.AraçOluþturmaKontrolu();


    }


    //Bu fonksýyonun amacý benim bir þeyler üretmek için envanterdeki kullanmýs oldugum eþyalarý
    // harcadýgýmda öðelistesini güncellýyor ve kac tane eþyam var onu tutuyor
    public void ÖðeListesiGüncelle()
    {
        ögeListesi.Clear();

        foreach (GameObject öge in slotListesi)
        {
            if (öge.transform.childCount > 0)
            {
                string öðeÝsmi = öge.transform.GetChild(0).name;
                string kloneKesme = "(Clone)";
                //Burda öðeÝsminde Clone adý vardý bunu ýstemedýgýmýz için Replace ile Clonu kaldýrdýk.
                string sonuç = öðeÝsmi.Replace(kloneKesme, "");

                ögeListesi.Add(sonuç);
            }

        }

    }



}

