using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvanterSistemiKontrolleriGUI : MonoBehaviour
{
    public static EnvanterSistemiKontrolleriGUI Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    //Envanterde aldıgımız seyın uzerıne geldıgınde ıslevının ne oldugunu soyleyen kısım  Canvasta Envanterın ıcındekı EnvanterÖgeBılgıEkranı yazan kısım
    public GameObject envanterÖğeBilgiEkranıUı;
    // I TUSUNA BASTIGIMIZDAKI ENVANTER ISTE CANVASTAKI ENVANTER YAZAN KISIM
    public GameObject envanterEkranUI;
    
    // yerden aldıgımız şeylerı gosteren, ekranda  ornegın tas aldıgımızda sol altta çıkan kısım.Canvasta bilgiEkranı yazan kısım
    public Image bilgiEkranı;
    // yerden aldıgımız şeylerı gosteren, ekranda  ornegın tas aldıgımızda sol altta çıkan kısım.Canvasta bilgiEkranı yazan kısımın içindeki text kısmı
    public Text ekleneninİsmi;
    // yerden aldıgımız şeylerı gosteren, ekranda  ornegın tas aldıgımızda sol altta çıkan kısım.Canvasta bilgiEkranı yazan kısımın içindeki resim kısmı
    public Image ekleneninResmi;
}
