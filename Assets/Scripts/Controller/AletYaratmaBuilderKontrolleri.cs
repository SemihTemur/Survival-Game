using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AletYaratmaBuilderKontrolleri
{
   public string item�smi;

    public string req1;
    public string req2;

    public int Req1say�s�;
    public int Req2say�s�;

    public int reqlerinSay�s�;

    public AletYaratmaBuilderKontrolleri(string name,int reqSay�,string R1,int R1num,string R2,int R2num)
    {
        item�smi = name;

        reqlerinSay�s� = reqSay�;

        req1 = R1;
        req2 = R2;

        Req1say�s�=R1num;
        Req2say�s�=R2num;
    }


}
