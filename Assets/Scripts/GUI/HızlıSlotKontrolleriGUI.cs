using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HızlıSlotKontrolleriGUI : MonoBehaviour
{
    public static HızlıSlotKontrolleriGUI Instance;

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

    // -- Kullanıcı Arayüzü -- canvastaki hızlıslot//
    public GameObject hızlıYuvaPaneli;

    //secilensayının rengını degıstırmek ıcın buna yanı parent'a ihtiyacım var.canvasta hızlıslotun orda
    public GameObject sayılarKlasörü;


}
