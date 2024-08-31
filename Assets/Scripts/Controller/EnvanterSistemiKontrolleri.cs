using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnvanterSistemiKontrolleri : MonoBehaviour
{
    // S�nglenton var burda
    public static EnvanterSistemiKontrolleri Instance { get; set; }

    public List<GameObject> slotListesi = new List<GameObject>();

    public List<string> �geListesi = new List<string>();

    private GameObject eklenecek�ge;

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
        // un�tyde b�r objen�n cocuklar�n� transform �bares�yle al�yorsun
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

            //E�er ���ilik ekran� a��ksa mouse ekranda g�z�ks�n diye bunu yap�yoruz.
            if (!���ilikSistemiKontrolleri.Instance.a��kM�)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            acikMi = false;
        }
    }


    public void EnvantereEkle(string itemName)
    {
        hangislotaEklensin = SonrakiBo�SlotBul();
        eklenecek�ge = Instantiate(Resources.Load<GameObject>(itemName), hangislotaEklensin.transform.position, hangislotaEklensin.transform.rotation);
        eklenecek�ge.transform.SetParent(hangislotaEklensin.transform);

        �geListesi.Add(itemName);

        StartCoroutine(BilgiEkran�n�G�ster(itemName, eklenecek�ge.GetComponent<Image>().sprite));

        ��eListesiG�ncelle();
        ���ilikSistemiKontrolleri.Instance.Ara�Olu�turmaKontrolu();
    }


    public IEnumerator BilgiEkran�n�G�ster(string name, Sprite sprite)
    {
        EnvanterSistemiKontrolleriGUI.Instance.bilgiEkran�.gameObject.SetActive(true);
        EnvanterSistemiKontrolleriGUI.Instance.eklenenin�smi.text = name;
        EnvanterSistemiKontrolleriGUI.Instance.ekleneninResmi.sprite = sprite;
        yield return new WaitForSeconds(5);
        EnvanterSistemiKontrolleriGUI.Instance.bilgiEkran�.gameObject.SetActive(false);
    }



    private GameObject SonrakiBo�SlotBul()
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


    public void EnvanterdenKald�r(string kald�r�l�cakItem, int kald�r�l�cakItemSay�s�)
    {
        int saya� = kald�r�l�cakItemSay�s�;
        for (int i = slotListesi.Count - 1; i >= 0; i--)
        {

            if (slotListesi[i].transform.childCount > 0)
            {

                if (slotListesi[i].transform.GetChild(0).name == kald�r�l�cakItem + "(Clone)" && saya� != 0)
                {

                    DestroyImmediate(slotListesi[i].transform.GetChild(0).gameObject);
                    saya�--;
                }

            }

        }

        ��eListesiG�ncelle();
        ���ilikSistemiKontrolleri.Instance.Ara�Olu�turmaKontrolu();


    }


    //Bu fonks�yonun amac� benim bir �eyler �retmek i�in envanterdeki kullanm�s oldugum e�yalar�
    // harcad�g�mda ��elistesini g�ncell�yor ve kac tane e�yam var onu tutuyor
    public void ��eListesiG�ncelle()
    {
        �geListesi.Clear();

        foreach (GameObject �ge in slotListesi)
        {
            if (�ge.transform.childCount > 0)
            {
                string ��e�smi = �ge.transform.GetChild(0).name;
                string kloneKesme = "(Clone)";
                //Burda ��e�sminde Clone ad� vard� bunu �stemed�g�m�z i�in Replace ile Clonu kald�rd�k.
                string sonu� = ��e�smi.Replace(kloneKesme, "");

                �geListesi.Add(sonu�);
            }

        }

    }



}

