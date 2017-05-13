using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "_ List", menuName = "Data Templates/Part Lister", order = 2)]
public class PartList : ScriptableObject
{
    public List<HullData> allHulls = new List<HullData>();
    public List<WeaponData> allWeapons = new List<WeaponData>();
    public List<MovementData> allMovements = new List<MovementData>();
    public List<GadgetData> allGadets = new List<GadgetData>();
}
