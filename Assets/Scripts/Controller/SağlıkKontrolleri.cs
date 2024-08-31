using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SağlıkKontrolleri : MonoBehaviour
{
    public static SağlıkKontrolleri Instance { get; private set;}

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

    private int can;
    private int düşmanHasarı;

    public Text sağlıkDurumu;

    public Slider slider;

    void Start()
    {
        can = 100;
        düşmanHasarı = 10;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            can -= düşmanHasarı;
            if (can >= 0)
            {
                float ondalıklıCan = can / 100f;
                sağlıkDurumu.text = can + "/" + 100;
                slider.value = ondalıklıCan;
            }
            else
            {
                can = 0;
            }
          

        }
        
    }

    public int getSağlık()
    {
        return can;
    }

    public void setSağlık(int canArttırma)
    {
        can = canArttırma;
        float ondalıklıCan = can / 100f;
        sağlıkDurumu.text = can + "/" + 100;
        slider.value = ondalıklıCan;
    }


 



}
