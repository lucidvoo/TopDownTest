using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    // массив префабов объектов
    [SerializeField] InteractiveObjectBase[] objectPrefabs;
    
    // коллайдер области
    private Collider areaCollider;
    // лист созданных объектов
    private List<InteractiveObjectBase> objects = new List<InteractiveObjectBase>();

    private void Start()
    {
        areaCollider = GetComponent<Collider>(); 
    }

    private void OnEnable()
    {
        Events.onInteractiveObjectDestroyed.AddListener(TryRemoveObject);
    }

    private void OnDisable()
    {
        Events.onInteractiveObjectDestroyed.RemoveListener(TryRemoveObject);
    }

    private void TryRemoveObject(InteractiveObjectBase objToRemove)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == objToRemove)
            {
                objects.RemoveAt(i);
                break;
            }
        }
    }

    // метод спавна случайного объекта из массива в случайном месте коллайдера
    public void SpawnRandomly()
    {
        InteractiveObjectBase objToSpawn = objectPrefabs[UnityEngine.Random.Range(0, objectPrefabs.Length)];
        Bounds bnds = areaCollider.bounds;
        Vector3 position = new Vector3(UnityEngine.Random.Range(bnds.min.x, bnds.max.x),
                                        objToSpawn.transform.position.y,
                                        UnityEngine.Random.Range(bnds.min.z, bnds.max.z));
        objects.Add(Instantiate(objToSpawn, position, objToSpawn.transform.rotation, transform));
    }


    // метод удаления всех созданных объектов
    public void DestroyObjects()
    {
        foreach (InteractiveObjectBase item in objects)
        {
            Destroy(item.gameObject);
        }
        objects.Clear();
    }

    // метод сохранения конфигурации объектов с перезаписью имеющейся
    public void SaveObjects()
    {
        Debug.LogError("Method hasn't realized yet!");
    }

    // метод загрузки сохраненной конфигурации, если она есть.
    public void LoadObjects()
    {
        Debug.LogError("Method hasn't realized yet!");
    }
}
