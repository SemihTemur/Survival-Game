using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSilmeKontrolleriGUI : MonoBehaviour
{
    public static ItemSilmeKontrolleriGUI Instance;
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
    // Canvastaký silme uyarýsý olan kýsým
    public GameObject copUyariUI;
    public Sprite cop_kapali;
    public Sprite cop_acik;

   
   

}
