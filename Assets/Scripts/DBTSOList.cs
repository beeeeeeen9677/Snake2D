using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBTSOList", menuName = "DBTList")]

public class DBTSOList : ScriptableObject
{
    public string scenario;
    public List<DBTSO> dbtList;
}
