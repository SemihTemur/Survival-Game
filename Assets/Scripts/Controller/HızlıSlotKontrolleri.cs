using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HızlıSlotKontrolleri : MonoBehaviour
{
    //Sınglenton
    public static HızlıSlotKontrolleri Instance { get; set; }


    //gameObjectlerı listeye atıp daha rahat işlem yapmak ıstıyorum
    public List<GameObject> hızlıYuvaListesi = new List<GameObject>();

    public int seçilenSayı = -1;
    private GameObject seçilenObje;

    public GameObject aletTutucu;

    private GameObject ıtemModel;

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

    private void Start()
    {
        YuvaListesiniDoldur();
    }


    private void Update()
    {
      //bu if elseif kısımları 1 2 3 falan tusuna bastıgımızda hızlıslotlardakı şeylerın olmasını saglıcak yanı 1 tusuna basınca ekrandakı 1 sayısı kırmızı olucak envantere suruklenemıcek
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           SeçilenHızlıSlot(1); 
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
           SeçilenHızlıSlot(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           SeçilenHızlıSlot(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
           SeçilenHızlıSlot(4);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
           SeçilenHızlıSlot(5);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
           SeçilenHızlıSlot(6);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
           SeçilenHızlıSlot(7);
        }

    }

    public void SeçilenHızlıSlot(int number)
    {
        //SeçilenSlotDoluysa rengını degıstırsın sayının. yoksa degıstırmesın ıstıyorum. yanı  o slotun ıcınde bır obje varsa rengı degıssın yoksa boş olan slotun sayı kısmının rengı neden degıssınkı?
        if (SeçilenSlotDoluMu(number) == true)
        {
            //  oncekı sayı sımdıkı sayıya esıt degılse gır
            if (seçilenSayı != number)
            {
                //bu fonksıyonu koymamın nedenı örneğin 1 tusuna bastıktan sonra 2 tusuna basarsan 1 tusundakı seçıldıMı daha oncesınde true oldugu ıcın sonra false olmuyor bu yuzden suruklebırak kısmı calısmıyordu o yuzden boyle yaptım hepsini false yapıyor bu sayede sadece kırmızı olanı true yapıyor
                seçildimiSıfırla();
                seçilenSayı = number;
                // hızlı slotun ıcındekı sprıte'ı alıyor.
                seçilenObje = seçilenObjeyıAl(number);
                seçilenObje.gameObject.GetComponent<ItemOzellikleriKontrolleri>().seçildiMi = true;

                // eger null degılse ıtemModel yanı Ornegın onceden baltaya eşitse sonra baltayı sahneden kaldıralım baska bır seye esıt olsun 
                if (ıtemModel != null)
                {
                    DestroyImmediate(ıtemModel);
                    ıtemModel = null;
                    // sahneden sılıp objeyı null'a eşitledıkten sonra yenısını olusturuyoruz
                    ModeliOluştur(seçilenObje);
                }
                // eger null ıse olustur dıyorum
                else
                {
                    //  slotun ıcındekı objeye gore model olusturucagız baltaysa balta modelını falan
                    ModeliOluştur(seçilenObje);
                }

                // burda sayılarKlasorünün number kısımlarına erişiyorum ve önce tüm hepsini beyaz yapıyorum
                foreach (Transform child in HızlıSlotKontrolleriGUI.Instance.sayılarKlasörü.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color = Color.white;
                }
                // sonra seçileni kırmızı yapıyorum
                Text değiştirilen = HızlıSlotKontrolleriGUI.Instance.sayılarKlasörü.transform.Find("number" + number).transform.Find("Text").GetComponent<Text>();
                değiştirilen.color = Color.red;

            }
            else
            {
                Text değiştirilen = HızlıSlotKontrolleriGUI.Instance.sayılarKlasörü.transform.Find("number" + number).transform.Find("Text").GetComponent<Text>();
                değiştirilen.color = Color.white;
                // burası -1 olmasaydı 1 geldıkten sonra sureklı 1e basılsaydı hep buraya gırerdı yanı ılk kez 1e basınca kırmızı olucak sonra bırdaha basınca beyaz olucak sonra bırdaha basınca tekrar kırmızı olmasını ıstıyorum fakat bunu yapmasaydım kırmızı olmaz beyaz olurdu yukarıdakı iften dolayı
                seçilenSayı = -1;
                //Önceki seçtiğimi false yapıp yeni seçtiğim objenin ItemOzellıklerını true yapmamı sağlıyor. Yani kısacası Örneğin ben 1.slotu seçmişsem ve sonra 2.slotu seçersem 1.slotu kapatmamı yarıyo gıbı bır şey yanı
                if (seçilenObje != null)
                {
                    seçilenObje.gameObject.GetComponent<ItemOzellikleriKontrolleri>().seçildiMi = false;
                   
                    //Eğer ben sectıgım ıtema bır daha basarsam elımde objemın yok edılmesını ıstıyorum
                    DestroyImmediate(ıtemModel);
                    ıtemModel = null;
                }


            }
        }

    }

    // secilen slot dolu degılse neden o kısımdakı sayı kırmızı olsun ki
    bool SeçilenSlotDoluMu(int sıra)
    {
        if (hızlıYuvaListesi[sıra-1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // once hepsı sıfırlansın sonra secılen true olsun
    public void seçildimiSıfırla()
    {
       for(int i = 0; i < hızlıYuvaListesi.Count; i++)
        {
            if (hızlıYuvaListesi[i].transform.childCount> 0)
            {
                hızlıYuvaListesi[i].transform.GetChild(0).GetComponent<ItemOzellikleriKontrolleri>().seçildiMi = false;
            }
        }
    }

    GameObject seçilenObjeyıAl(int number)
    {
        return hızlıYuvaListesi[number - 1].transform.GetChild(0).gameObject;
    }

    public void ModeliOluştur(GameObject seçilenModel)
    {
        // olusurken Unıty Clone ıbaresınıde eklıyor ya onu çıkarmamız lazım o yuzden bunu yapıyoruz
        string seçilenModelinİsmi = seçilenModel.name.Replace("(Clone)", "");
        seçilenModelinİsmi += "_Model";
        ıtemModel = Instantiate(Resources.Load<GameObject>(seçilenModelinİsmi), new Vector3(0f, 0, 0f), Quaternion.Euler(-10.29f, 270f, 90f));
        ıtemModel.transform.SetParent(aletTutucu.transform, false);

    }

    private void YuvaListesiniDoldur()
    {
        foreach (Transform çocuk in HızlıSlotKontrolleriGUI.Instance.hızlıYuvaPaneli.transform)
        {
           if (çocuk.CompareTag("HızlıYuva"))
           {
               hızlıYuvaListesi.Add(çocuk.gameObject);
           }
        }
    }

    public void HızlıYuvayaEkle(GameObject ekipman)
    {
        // Boş yuva bul
        GameObject boşYuva = BoşYuvaBul();
        // Nesnemizin transformunu ayarla
        ekipman.transform.SetParent(boşYuva.transform, false);
        EnvanterSistemiKontrolleri.Instance.ÖğeListesiGüncelle();
    }

    private GameObject BoşYuvaBul()
    {
        foreach (GameObject yuva in hızlıYuvaListesi)
        {
            if (yuva.transform.childCount == 0)
            {
                    return yuva;
            }
        }
            return new GameObject();
    }

    // tamamı mı dolu mu bunun kontrolunu yapar
    public bool DoluMu()
    {
        int sayac = 0;

        foreach (GameObject yuva in hızlıYuvaListesi)
        {
            if (yuva.transform.childCount > 0)
            {
                    sayac += 1;
            }
        }

        return sayac == 7;

    }


}


