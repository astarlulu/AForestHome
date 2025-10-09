using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu]

public class ObjectsDatabase : ScriptableObject
{
    public List<ObjectData> objectsData;

}


[Serializable]

public class ObjectData
{
    [field: SerializeField] //To display in inspector
    public string Name { get; private set; } // Name of the object - private to set it through the inspector but nothing outside of class can change it.
    [field: SerializeField]

    public int ID { get; private set; } // Unique ID defining this object - needs this for system to work properly.
    [field: SerializeField]

    public Vector2Int Size { get; private set; } = Vector2Int.one; // Each object has a size - smallest is 1x1
    [field: SerializeField]

    public GameObject Prefab { get; private set; } // Reference to the 3D asset prefabs assign here to data

}
