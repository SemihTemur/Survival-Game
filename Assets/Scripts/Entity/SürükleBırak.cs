using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SürükleBırak : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
//elemanlarının konumunu, boyutunu ve dönüşünü kontrol etmek için kullanılan bir bileşendir.
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    // static degıskenler Inspector penceresınde gozukmezler
    public static GameObject sürüklenenÖğe;
    Vector3 başlangıçKonumu;
    Transform başlangıçEbeveyn;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // sürüklemenin başladığı kısım
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
 // objeye tıklanılmasını saglıyor ve sürüklenmesını saglıyor.
        canvasGroup.blocksRaycasts = false;
        başlangıçKonumu = transform.position;
        başlangıçEbeveyn = transform.parent;
        // burda hem oncekı degerın parentını tutuyor hemde en usttekını yanı canvası
        // cunku bos bır yere bırakırsa objeyı canvastan dolayı baslangıcEbeveynıne gıtmesını saglıyor.
        transform.SetParent(transform.root);
        sürüklenenÖğe = gameObject;
    }

    // sürüklemenin devam ettiği kısım
    public void OnDrag(PointerEventData eventData)
    {
        // Bu sayede öğe fare ile birlikte (aynı hızda) hareket eder ve eğer canvas'ın ölçeği farklıysa (1 dışında), tutarlı bir şekilde olur.
        rectTransform.anchoredPosition += eventData.delta;
    }

    // sürüklemenin bittiği kısım
   public void OnEndDrag(PointerEventData eventData)
    {
        sürüklenenÖğe = null;

        if (transform.parent == başlangıçEbeveyn || transform.parent == transform.root)
        {
            transform.position = başlangıçKonumu;
            transform.SetParent(başlangıçEbeveyn);
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

   
}
