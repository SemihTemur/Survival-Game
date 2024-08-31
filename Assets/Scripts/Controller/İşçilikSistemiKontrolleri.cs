using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class İşçilikSistemiKontrolleri : MonoBehaviour
{
    public static İşçilikSistemiKontrolleri Instance { get; private set; }

    // bunu oluşturmamın nedeni Envanterdekı  öğeListesini bunun içine atıp kontrol yapmak için
    public List<string> ÖğeListesiEnvanter = new List<string>();
    //İşcilikEkranın ordaki butonu alıcagız
    Button araçlar;

    //KategoriAraçlarEkranıUıdakı butonu alıcagız 
    Button baltaOluştur;

    //KategoriAraçlarEkranıUıdakı yazıları alıcagız 





    Text baltaReq, baltaReq1;

    // Ekranlar Açıkmı değilmi kontrol yapıcak ona gore dıger scrıptlerde bazı ozellıkler degısıcek
    public bool açıkMı;

    public AletYaratmaBuilderKontrolleri aletOluştur = new AletYaratmaBuilderKontrolleri("Balta", 2, "Taş", 3, "Tahta", 3);

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
        açıkMı = false;

        //transform bileşeni bizim  cocuklarımıza erişmemizi sağlıyor
        araçlar = İşçilikSistemiKontrolleriGUI.Instance.işçilikEkranıUı.transform.Find("AraçlarButonu").GetComponent<Button>();
        araçlar.onClick.AddListener(delegate { AçKategoriEkranı(); });

        baltaReq = İşçilikSistemiKontrolleriGUI.Instance.kategoriEkranıUı.transform.Find("Balta").transform.Find("req").GetComponent<Text>();
        baltaReq1 = İşçilikSistemiKontrolleriGUI.Instance.kategoriEkranıUı.transform.Find("Balta").transform.Find("req2").GetComponent<Text>();

        baltaOluştur = İşçilikSistemiKontrolleriGUI.Instance.kategoriEkranıUı.transform.Find("Balta").transform.Find("Oluştur").GetComponent<Button>();
        baltaOluştur.onClick.AddListener(delegate { ItemOluştur(aletOluştur); });

    }


    void AçKategoriEkranı()
    {
        İşçilikSistemiKontrolleriGUI.Instance.işçilikEkranıUı.SetActive(false);
        İşçilikSistemiKontrolleriGUI.Instance.kategoriEkranıUı.SetActive(true);
    }

    void ItemOluştur(AletYaratmaBuilderKontrolleri alet)
    {
        Debug.Log("ds");
        // örneğin balta oluşturdugumuzda bunu envantere eklıyıcegız ya ondan bu lazım
        EnvanterSistemiKontrolleri.Instance.EnvantereEkle(alet.itemİsmi);

        // ornegın balta olustururken tas ve tahtayı kullanıcagız ya  kullandıktan sonra onları envanterden kaldırmamız lazım
        if (alet.reqlerinSayısı == 1)
        {
            EnvanterSistemiKontrolleri.Instance.EnvanterdenKaldır(alet.req1, alet.Req1sayısı);
        }
        else if (alet.reqlerinSayısı == 2)
        {
            EnvanterSistemiKontrolleri.Instance.EnvanterdenKaldır(alet.req1, alet.Req1sayısı);
            EnvanterSistemiKontrolleri.Instance.EnvanterdenKaldır(alet.req2, alet.Req2sayısı);
        }

        StartCoroutine(hesapla());

    }


    public IEnumerator hesapla()
    {
        yield return 0f;

        // envanterden kaldırdıktan sonra listeyi guncellememız lazım onceden 6 taş varsa  sonra 3 taş kalıyor ya envanterde öğelistesindende 3 tane kalması lazım
        EnvanterSistemiKontrolleri.Instance.ÖğeListesiGüncelle();
       
        //Bu ise ornegın bizim balta olusturmamız ıcın 3 taşa ıhtıyacımız mı var onun kontrolunu yapıcak bakıcak 3 tane tasımız varsa balta olustur dıyıcek yanı ornek bu
        AraçOluşturmaKontrolu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !açıkMı)
        {
            İşçilikSistemiKontrolleriGUI.Instance.işçilikEkranıUı.SetActive(true);
            açıkMı = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else if (Input.GetKeyDown(KeyCode.C) && açıkMı)
        {
            İşçilikSistemiKontrolleriGUI.Instance.işçilikEkranıUı.SetActive(false);
            İşçilikSistemiKontrolleriGUI.Instance.kategoriEkranıUı.SetActive(false);

            //Eğer Envanter ekranı açıksa mouse ekranda gözüksün diye bunu yapıyoruz.
            if (!EnvanterSistemiKontrolleri.Instance.acikMi)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            açıkMı = false;
        }

    }


    public void AraçOluşturmaKontrolu()
    {
        int taş_sayısı = 0;
        int tahta_sayısı = 0;

        ÖğeListesiEnvanter = EnvanterSistemiKontrolleri.Instance.ögeListesi;

        foreach (string öğe in ÖğeListesiEnvanter)
        {
            switch (öğe)
            {
                case "Taş":
                    taş_sayısı++;
                    break;
                case "Tahta":
                    tahta_sayısı++;
                    break;
            }

        }

        baltaReq.text = "3 Taş [" + taş_sayısı + "]";
        baltaReq1.text = "3 Tahta [" + tahta_sayısı + "]";

        if (taş_sayısı >= 3 && tahta_sayısı >= 3)
        {
            baltaOluştur.gameObject.SetActive(true);
        }
        else
        {
            baltaOluştur.gameObject.SetActive(false);
        }

    }





}
