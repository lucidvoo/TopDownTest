using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjSaveData
{
    private float x, y, z;
    private Type objType;

    public float X => x;
    public float Y => y;
    public float Z => z;
    public Type ObjType => objType;

    public ObjSaveData(Vector3 pos, Type objType)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
        this.objType = objType;
    }
}
