using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ufo settings", menuName = "Ufos/Create")]
public class UfoSettings : ScriptableObject
{
    public float Speed;
    public float ShootCoolDown;
    public int ScoreReward;
    public Ufo Prefab;
}
