using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtkileşimliObje : MonoBehaviour
{
    public string ItemName;
    public bool oyuncuAlandamı;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && oyuncuAlandamı && SeçimYöneticisiKontrolleri.Instance.hedef&& SeçimYöneticisiKontrolleri.Instance.etkileşimliObje==gameObject)
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
            oyuncuAlandamı = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandamı = false;
        }
    }

}
