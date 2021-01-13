using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New asteroid settings", menuName = "Asteroids/Create")]
public class AsteroidSettings : ScriptableObject
{
    public int HealthPoints;
    [Range(0.001f,100)]
    public float Speed;
    public Asteroid Prefab;
    public List<AsteroidSettings> AfterDeathAsteroids;
    public int ScoreReward;
}
