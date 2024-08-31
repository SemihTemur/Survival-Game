using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etkile�imliObje : MonoBehaviour
{
    public string ItemName;
    public bool oyuncuAlandam�;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && oyuncuAlandam� && Se�imY�neticisiKontrolleri.Instance.hedef&& Se�imY�neticisiKontrolleri.Instance.etkile�imliObje==gameObject)
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
            oyuncuAlandam� = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuAlandam� = false;
        }
    }

}
