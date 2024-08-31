using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class HayvanHareketi : MonoBehaviour
{
    // animasyon vermemizi saðlýyor
    Animator animator;

    //hayvanýmýza hareket verýyor
    public float hareketHizi = 0.2f;

    // hayvanýmýz durdugunda onun posýtýon degerlerýný almamýzý saglýyor.
    Vector3 durmaKonumu;

    // hayvanýmýz bellý zamanlarda yuruyucek ya onun ýcýn var bu yurumesayacý sýfýrlandýgýnda bundaký degerý yurume sayacýna atýcagýz.
    float yurumeZamani;

    //  hayvanýmýz belli zamanlardaký yurume sayacýný tutuyor
    public float yurumeSayaci;

    // hayvanýmýz belli zamanlarda durucak ya onu tutuyor bu ve bazen yurume sayacý sýfýr oldugunda burdaký degerý ona atýcagýz.
    float beklemeZamani;

  // durma sayacý
    public float beklemeSayaci;

    // adý ustunde zaten
    int yurumeYonu;
    int yurumeYonuYedek;

    public bool yuruyor;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Tüm prefab'lerin ayný anda hareket etmemesi için
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

                // Bekleme sayacýný sýfýrla
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

    // bu eðer hayvan taþa ya da baska bir objeye carparsa yön deðiþsin diye yazýldý
    private void OnCollisionEnter(Collision collision)
    {
        yurumeYonu = Random.Range(0, 4);
        while (yurumeYonuYedek == yurumeYonu)
        {
            yurumeYonu = Random.Range(0, 4);
        }

    }

}
