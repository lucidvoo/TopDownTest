using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// логика работы отдельной области и ее кнопок. Сохранение/загрузка, спавн объектов.

public class AreaController : MonoBehaviour
{
    [SerializeField] InteractiveObjectBase[] objectPrefabs;
    
    // коллайдер области для того, чтобы спавнить объекты внутри нее
    private Collider areaCollider;
    // список созданных объектов, чтобы можно было их удалить или сохранить
    private List<InteractiveObjectBase> objects = new List<InteractiveObjectBase>();
    private string pathToSave;


    private void Start()
    {
        areaCollider = GetComponent<Collider>();

        pathToSave = Path.Combine(Application.persistentDataPath, gameObject.name + ".sav");
    }


    private void OnEnable()
    {
        Events.onInteractiveObjectDestroyed.AddListener(TryRemoveObject);
    }


    private void OnDisable()
    {
        Events.onInteractiveObjectDestroyed.RemoveListener(TryRemoveObject);
    }


    // если объект удален(например, разрушаемый объект), то нужно обновить массив объектов
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


    // метод спавна случайного объекта из массива префабов в случайном месте коллайдера
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


    #region Save & Load

    // сохранение текущей конфигурации объектов в области
    public void SaveObjects()
    {
        // массив информации по каждому объекту, который будем записывать в файл.
        ObjSaveData[] objSaves = new ObjSaveData[objects.Count];

        for (int i = 0; i < objSaves.Length; i++)
        {
            objSaves[i] = objects[i].GetSaveData();
        }
        
        SaveToFile(objSaves);
    }


    private void SaveToFile(ObjSaveData[] objSaves)
    {
        FileStream fStream = new FileStream(pathToSave, FileMode.Create);

        BinaryFormatter bFormatter = new BinaryFormatter();
        bFormatter.Serialize(fStream, objSaves);

        fStream.Close();
    }


    // метод загрузки сохраненной конфигурации, если она есть.
    public void LoadObjects()
    {
        ObjSaveData[] objSaves = LoadFromFile();

        // null будет в случае если файла нет
        if (objSaves == null)
        {
            return;
        }

        // удалим все имеющиеся объекты и создадим новые
        DestroyObjects();

        foreach (ObjSaveData save in objSaves)
        {
            InteractiveObjectBase objToSpawn = null;
            // нужно найти к какому типу относится объект из сохранения и выбрать соотв. префаб
            for (int i = 0; i < objectPrefabs.Length; i++)
            {
                if (Equals(save.ObjType, objectPrefabs[i].GetType()))
                {
                    objToSpawn = objectPrefabs[i];
                    break;
                }
            }

            if(objToSpawn == null)
            {
                Debug.LogError("Cant find needed type.");
            }

            objects.Add(Instantiate(objToSpawn, new Vector3(save.X, save.Y, save.Z), objToSpawn.transform.rotation, transform));
        }
    }


    private ObjSaveData[] LoadFromFile()
    {
        if (!File.Exists(pathToSave))
        {
            Debug.Log("Save file not found in " + pathToSave);
            return null;
        }

        FileStream fStream = new FileStream(pathToSave, FileMode.Open);

        BinaryFormatter bFormatter = new BinaryFormatter();
        ObjSaveData[] objSaves = bFormatter.Deserialize(fStream) as ObjSaveData[];

        fStream.Close();

        return objSaves;
    }
    #endregion
}
