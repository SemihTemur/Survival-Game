using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AletYaratmaBuilderKontrolleri
{
   public string itemİsmi;

    public string req1;
    public string req2;

    public int Req1sayısı;
    public int Req2sayısı;

    public int reqlerinSayısı;

    public AletYaratmaBuilderKontrolleri(string name,int reqSayı,string R1,int R1num,string R2,int R2num)
    {
        itemİsmi = name;

        reqlerinSayısı = reqSayı;

        req1 = R1;
        req2 = R2;

        Req1sayısı=R1num;
        Req2sayısı=R2num;
    }


}
