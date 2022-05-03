using System;
using UnityEngine;

// класс хранилище дл€ данных, необходимых дл€ сохренени€/загрузки объекта
// массив экземпл€ров этого класса будет записан в файл при сохранении.

[Serializable]
public class ObjSaveData
{
    // позици€
    private float x, y, z;
    // тип интерактивного объекта дл€ того чтобы узнать из какого префаба его воссоздавать при загрузке
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
