using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtkileþimliObje : MonoBehaviour
{
    public string ItemName;
    public bool oyuncuAlandamý;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && oyuncuAlandamý && SeçimYöneticisiKontrolleri.Instance.hedef&& SeçimYöneticisiKontrolleri.Instance.etkileþimliObje==gameObject)
        {

           if (!EnvanterSistemiKontrolleri.Instance.DolumuKontrolu())
           {
              EnvanterSistemiKontrolleri.Instance.EnvantereEkle(ItemName);
              Destroy(this.gameObject);
           }
           else
           {
                Debug.Log("Envanter Doludur.");
           }

        } 
    }

    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandamý = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandamý = false;
        }
    }

}
