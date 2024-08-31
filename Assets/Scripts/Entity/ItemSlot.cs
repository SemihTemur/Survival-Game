using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler
{
   
  
    public GameObject Item
    {
        get
        {
            // eger Slotumun alt�nda obje varsa k� her slotun alt�nda 1 obje olur
            // onun ozell�kler�n� transformunu als�n gameObjem
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
// yoksa zaten null doner
            return null;
        }
    }

 


    // Itemslot scr�pt�n�n bulundugu objeye surukled�g�m sey� b�rakt�g�m anda buras� devreye g�r�yor b�rakt�g�m anda el�m� moustan cekt�g�m anda
    // not bu onDrop  OnEndDragtan daha once cal�s�r
    public void OnDrop(PointerEventData eventData)
    {

        //eger objem null ise
        if (!Item)
        {   
          
         

            // burda bu kontrolu yapmam�n neden� ItemSlot class�na sah�p �k� tane k�s�m var b�r�s� Envanter�n �c�ndek�ler d�ger�de h�zl�Slotun �c�ndek�ler �k�s�de bu class� kulland�g� �c�n h�zl�Slota suruklersem buras� cal�smass�n d�ye yapt�k
            if (transform.CompareTag("H�zl�Yuva") == false)
            {
                S�r�kleB�rak.s�r�klenen��e.transform.SetParent(transform);
                S�r�kleB�rak.s�r�klenen��e.transform.localPosition = new Vector2(0, 0);
                //�imdi e�er  ben h�zl�Slota surukled�g�m b�r
                //sey� tekrardan envantere suruklersem h�zl�Slottan g�d�ceg� �c�m objem �uanEkiplend�y� false yapmam laz�m. �unku yapmassam e�er  envantere surukled�kten sonra tekrar h�zl�Slota surukleyemem
                S�r�kleB�rak.s�r�klenen��e.GetComponent<ItemOzellikleriKontrolleri>().h�zl�Slotun��indeMi = false;
                //H�zl� yuvada b�r sey ald�g�m�zda Envanter�n yenilenmes�n� �st�yoruz cunku h�zl�yuvadan ald�g�m�zda envanter�n �cer�s�n�n deger� guncellenmel� ekleme yap�yoruz cunku
                EnvanterSistemiKontrolleri.Instance.��eListesiG�ncelle();
                ���ilikSistemiKontrolleri.Instance.Ara�Olu�turmaKontrolu();

            }
            // eger objem� h�zl�yuva k�sm�na suruklersem yan� h�zl�Slot k�sm�na ozaman true olsun 
            else if (transform.CompareTag("H�zl�Yuva") == true&& S�r�kleB�rak.s�r�klenen��e.GetComponent<ItemOzellikleriKontrolleri>().EkiplenebilirMi)
            {
                S�r�kleB�rak.s�r�klenen��e.transform.SetParent(transform);
                S�r�kleB�rak.s�r�klenen��e.transform.localPosition = new Vector2(0, 0);
                //�imdi e�er  ben h�zl�Slota surukled�g�m b�r sey� tekrardan envantere suruklersem h�zl�Slottan g�d�ceg� �c�m objem �uanEkiplend�y� false yapmam laz�m. �unku yapmassam e�er  envantere surukled�kten sonra tekrar h�zl�Slota surukleyemem
                S�r�kleB�rak.s�r�klenen��e.GetComponent<ItemOzellikleriKontrolleri>().h�zl�Slotun��indeMi = true;
                //Envanterden b�r sey ald�g�m�zda Envanter�n yenilenmes�n� �st�yoruz cunku envanterden ald�g�m�zda envanter�n �cer�s�n�n deger� guncellenmel� azalma yap�yoruz cunku
                EnvanterSistemiKontrolleri.Instance.��eListesiG�ncelle();
                ���ilikSistemiKontrolleri.Instance.Ara�Olu�turmaKontrolu();
            }

        }

    }

}
