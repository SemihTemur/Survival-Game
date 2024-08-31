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
            // eger Slotumun altında obje varsa kı her slotun altında 1 obje olur
            // onun ozellıklerını transformunu alsın gameObjem
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
// yoksa zaten null doner
            return null;
        }
    }

 


    // Itemslot scrıptının bulundugu objeye surukledıgım seyı bıraktıgım anda burası devreye gırıyor bıraktıgım anda elımı moustan cektıgım anda
    // not bu onDrop  OnEndDragtan daha once calısır
    public void OnDrop(PointerEventData eventData)
    {

        //eger objem null ise
        if (!Item)
        {   
          
         

            // burda bu kontrolu yapmamın nedenı ItemSlot classına sahıp ıkı tane kısım var bırısı Envanterın ıcındekıler dıgerıde hızlıSlotun ıcındekıler ıkısıde bu classı kullandıgı ıcın hızlıSlota suruklersem burası calısmassın dıye yaptık
            if (transform.CompareTag("HızlıYuva") == false)
            {
                SürükleBırak.sürüklenenÖğe.transform.SetParent(transform);
                SürükleBırak.sürüklenenÖğe.transform.localPosition = new Vector2(0, 0);
                //Şimdi eğer  ben hızlıSlota surukledıgım bır
                //seyı tekrardan envantere suruklersem hızlıSlottan gıdıcegı ıcım objem ŞuanEkiplendıyı false yapmam lazım. çunku yapmassam eğer  envantere surukledıkten sonra tekrar hızlıSlota surukleyemem
                SürükleBırak.sürüklenenÖğe.GetComponent<ItemOzellikleriKontrolleri>().hızlıSlotunİçindeMi = false;
                //Hızlı yuvada bır sey aldıgımızda Envanterın yenilenmesını ıstıyoruz cunku hızlıyuvadan aldıgımızda envanterın ıcerısının degerı guncellenmelı ekleme yapıyoruz cunku
                EnvanterSistemiKontrolleri.Instance.ÖğeListesiGüncelle();
                İşçilikSistemiKontrolleri.Instance.AraçOluşturmaKontrolu();

            }
            // eger objemı hızlıyuva kısmına suruklersem yanı hızlıSlot kısmına ozaman true olsun 
            else if (transform.CompareTag("HızlıYuva") == true&& SürükleBırak.sürüklenenÖğe.GetComponent<ItemOzellikleriKontrolleri>().EkiplenebilirMi)
            {
                SürükleBırak.sürüklenenÖğe.transform.SetParent(transform);
                SürükleBırak.sürüklenenÖğe.transform.localPosition = new Vector2(0, 0);
                //Şimdi eğer  ben hızlıSlota surukledıgım bır seyı tekrardan envantere suruklersem hızlıSlottan gıdıcegı ıcım objem ŞuanEkiplendıyı false yapmam lazım. çunku yapmassam eğer  envantere surukledıkten sonra tekrar hızlıSlota surukleyemem
                SürükleBırak.sürüklenenÖğe.GetComponent<ItemOzellikleriKontrolleri>().hızlıSlotunİçindeMi = true;
                //Envanterden bır sey aldıgımızda Envanterın yenilenmesını ıstıyoruz cunku envanterden aldıgımızda envanterın ıcerısının degerı guncellenmelı azalma yapıyoruz cunku
                EnvanterSistemiKontrolleri.Instance.ÖğeListesiGüncelle();
                İşçilikSistemiKontrolleri.Instance.AraçOluşturmaKontrolu();
            }

        }

    }

}
