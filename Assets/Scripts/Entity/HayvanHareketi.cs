using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HayvanHareketi : MonoBehaviour
{
    // animasyon vermemizi sa�l�yor
    Animator animator;

    //hayvan�m�za hareket ver�yor
    public float hareketHizi = 0.2f;

    // hayvan�m�z durdugunda onun pos�t�on degerler�n� almam�z� sagl�yor.
    Vector3 durmaKonumu;

    // hayvan�m�z bell� zamanlarda yuruyucek ya onun �c�n var bu yurumesayac� s�f�rland�g�nda bundak� deger� yurume sayac�na at�cag�z.
    float yurumeZamani;

    //  hayvan�m�z belli zamanlardak� yurume sayac�n� tutuyor
    public float yurumeSayaci;

    // hayvan�m�z belli zamanlarda durucak ya onu tutuyor bu ve bazen yurume sayac� s�f�r oldugunda burdak� deger� ona at�cag�z.
    float beklemeZamani;

  // durma sayac�
    public float beklemeSayaci;

    // ad� ustunde zaten
    int yurumeYonu;
    int yurumeYonuYedek;

    public bool yuruyor;

    void Start()
    {
        animator = GetComponent<Animator>();

        // T�m prefab'lerin ayn� anda hareket etmemesi i�in
        yurumeZamani = Random.Range(3, 6);
        beklemeZamani = Random.Range(5, 7);

        beklemeSayaci = beklemeZamani;
        yurumeSayaci = yurumeZamani;

        YonSec();
    }

    void Update()
    {
        if (yuruyor)
        {
            animator.SetBool("isRunning", true);

            yurumeSayaci -= Time.deltaTime;

            switch (yurumeYonu)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * hareketHizi * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * hareketHizi * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * hareketHizi * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * hareketHizi * Time.deltaTime;
                    break;
            }

            if (yurumeSayaci <= 0)
            {
                durmaKonumu = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                yuruyor = false;

                // Hareketi durdur
                transform.position = durmaKonumu;
                animator.SetBool("isRunning", false);

                // Bekleme sayac�n� s�f�rla
                beklemeSayaci = beklemeZamani;
            }

        }


        else
        {
            beklemeSayaci -= Time.deltaTime;

            if (beklemeSayaci <= 0)
            {
                YonSec();
            }
        }


    }

    public void YonSec()
    {
        yurumeYonu = Random.Range(0, 4);
        yurumeYonuYedek = yurumeYonu;
        yuruyor = true;
        yurumeSayaci = yurumeZamani;
    }

    // bu e�er hayvan ta�a ya da baska bir objeye carparsa y�n de�i�sin diye yaz�ld�
    private void OnCollisionEnter(Collision collision)
    {
        yurumeYonu = Random.Range(0, 4);
        while (yurumeYonuYedek == yurumeYonu)
        {
            yurumeYonu = Random.Range(0, 4);
        }

    }

}
