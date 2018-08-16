using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrebuildedFences
{

    private static readonly int[] Fence1=
    {
        0,0,0,0,1,0,0,0,0
    };
    
    private static readonly int[] Fence2=
    {
        1,0,0,1,0,1,0,0,1
    };
    
    private static readonly int[] Fence3=
    {
        1,0,0,1,1,1,0,0,1
    };
    
    private static readonly int[] Fence4=
    {
        0,0,1,1,1,1,1,0,0
    };
    
    private static readonly int[] Fence5=
    {
        1,1,0,0,1,0,0,1,1
    };
    
    private static readonly int[] Fence6=
    {
        1,1,0,0,1,0,0,1,1
    };

    public static List<int[]> FenceList = new List<int[]>()
    {
        Fence1,Fence2,Fence3,Fence4,Fence5,Fence6
    };
}
