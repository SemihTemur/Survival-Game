using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeçimYöneticisiKontrolleriGUI : MonoBehaviour
{
    public static SeçimYöneticisiKontrolleriGUI Instance;
    //GameObject olarak degılde dırekt Text olarakta alabılırdık ama bu kullanım daha esnek bır kullanım oluyor cunku Textın ıcındekı butona erısmek ıstıyorsak gameobject sayesınde erısebılırız text olsaydı erısemezdık
    public Text interaction_Info_UI;
    public Image nokta;
    public Image el;
    public GameObject ağacBilgileri;
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
}
