using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Se�imY�neticisiKontrolleri: MonoBehaviour
{
    public static Se�imY�neticisiKontrolleri Instance {get; private set;}//burda design patternlerden singleton �lkes� var
    private Text interaction_text;
    public  bool hedef;
    public GameObject etkile�imliObje;
    // el g�runuyorsa ben�m baltam�n an�masyonu cal�smas�n cunku baltam�n an�masyonu sol t�ka bas�nca cal�s�yor ama ayn� zamanda envantere b�sey ekl�y�ceg�m�z zamanda sol t�ka bas�yoruz onun kontrolu bu. bunu ModelKontrolleri scr�pt�nde cag�r�cam
    public bool elG�r�n�yorsa;
    public GameObject se�ilenA�ac;
    
    
    private void Start()
    {
        hedef = false;
        interaction_text = Se�imY�neticisiKontrolleriGUI.Instance.interaction_Info_UI.GetComponent<Text>();// i�indeki yaz�ya eri�memizi sa�l�yor bu kod sadece yaz�ya eri�ebiliyoruz.
        
        
    }

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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// Ekran �zerindek� noktan�n koord�natlar�n� b�ze ver�r
        RaycastHit hit;

        //Envanter� act�g�m�zda ���n gondermey� b�raks�n d�ye bunu yapt�k !EnvanterSistemi.Instance.acikMi  cunku envanter ac�kken b�le yerdek� tas� falan al�yor
        if (Physics.Raycast(ray, out hit)&&!EnvanterSistemiKontrolleri.Instance.acikMi)//nesne varsa buraya g�rer ama gokyuzu b�r nesne deg�l o yuzden else k�sm�n� ekled�m cunku else k�sm� olmasayd� b�r nesneye denk geld�kten sonra nesne olmayan b�r seye denk geld�g�nde yaz� kapanm�yordu
        {
            var selectionTransform = hit.transform;
           //referans al�yoruz burda bu sayede Etkile�imliObjen�n �cer�s�ndek� degerlere er�seb�l�yoruz.
            Etkile�imliObje se�ilenObje = selectionTransform.GetComponent<Etkile�imliObje>();

            A�acKesmeKontrolleri a�acKesme = selectionTransform.GetComponent<A�acKesmeKontrolleri>();
            // g�nderdi�im a�ac ���na geliyorsa ve oyuncuda alandaysa
            //A�a�lar i�in
            if (a�acKesme && a�acKesme.oyuncuAlandaM�)
            {
                a�acKesme.kesilebilirMi = true;
                se�ilenA�ac = a�acKesme.gameObject;
                Se�imY�neticisiKontrolleriGUI.Instance.a�acBilgileri.gameObject.SetActive(true);
            }

            //E�er oyuncu Alanda de�ilse ama ���n agaca dey�yorsa
            else
            {
                // yan� ornegin;ben ilk ba�ta alana girdi�imde se��lenAgac null olmiyacak ama sonra alandan c�karsam null olmas�n� �sted�g�m �c�n bunu ekl�yorum
                if (se�ilenA�ac != null)
                {
                    se�ilenA�ac.GetComponent<A�acKesmeKontrolleri>().kesilebilirMi = false;
                    se�ilenA�ac = null;
                    Se�imY�neticisiKontrolleriGUI.Instance.a�acBilgileri.gameObject.SetActive(false);
                }
                else
                {
                    se�ilenA�ac = null;
                    Se�imY�neticisiKontrolleriGUI.Instance.a�acBilgileri.gameObject.SetActive(false);
                }
            }

            // eklenebilenler �c�n
            if (se�ilenObje &&se�ilenObje.gameObject.CompareTag("Eklenebilir") && se�ilenObje.oyuncuAlandam�)
            {

     // bu hedef boolen deg�sken� tum scr�ptlerde true oldugu �c�n d�ger scr�ptlerde oyuncualandam� true olursa d�ger heps�de raycast ustler�ne gelmeden yok olur bunun �c�n raycaste gelen k�sm�n gameObjec�n� al�p onu raycaste gelen k�s�mdak� objen�n scr�pt�yle es�tlemem�z laz�m.
                hedef = true;
                elG�r�n�yorsa = true;
                etkile�imliObje = se�ilenObje.transform.gameObject;
                interaction_text.text = se�ilenObje.GetItemName();

                Se�imY�neticisiKontrolleriGUI.Instance.interaction_Info_UI.gameObject.SetActive(true);
                Se�imY�neticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(false);
                Se�imY�neticisiKontrolleriGUI.Instance.el.gameObject.SetActive(true);
            }

            else
            {
                hedef = false;
                elG�r�n�yorsa = false;
                Se�imY�neticisiKontrolleriGUI.Instance.interaction_Info_UI.gameObject.SetActive(false);
                Se�imY�neticisiKontrolleriGUI.Instance.el.gameObject.SetActive(false);
                Se�imY�neticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(true);
            }

        }

        else // nesne yoksa buraya g�rer
        {
            se�ilenA�ac = null;
            Se�imY�neticisiKontrolleriGUI.Instance.a�acBilgileri.gameObject.SetActive(false);
            hedef = false;
            elG�r�n�yorsa = false;
            se�ilenA�ac = null;
            Se�imY�neticisiKontrolleriGUI.Instance.interaction_Info_UI.gameObject.SetActive(false);
            Se�imY�neticisiKontrolleriGUI.Instance.el.gameObject.SetActive(false);
            if(EnvanterSistemiKontrolleri.Instance.acikMi || ���ilikSistemiKontrolleri.Instance.a��kM�)
            {
                Se�imY�neticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(false);
            }
            else
            {
                Se�imY�neticisiKontrolleriGUI.Instance.nokta.gameObject.SetActive(true);
            }
           
        }

    }





}
