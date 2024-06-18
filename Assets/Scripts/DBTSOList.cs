using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DBTSOList", menuName = "DBTList")]

public class DBTSOList : ScriptableObject
{
    public string mood;
    public string scenario;
    public List<DBTData> dbtList;
}
