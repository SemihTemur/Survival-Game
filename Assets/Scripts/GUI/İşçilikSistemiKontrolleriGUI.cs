using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class İşçilikSistemiKontrolleriGUI : MonoBehaviour
{
    public static İşçilikSistemiKontrolleriGUI Instance;
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

    public GameObject işçilikEkranıUı;

    public GameObject kategoriEkranıUı;

}
