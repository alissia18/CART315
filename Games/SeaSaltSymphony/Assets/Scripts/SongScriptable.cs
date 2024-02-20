using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Song", menuName = "CustomData/Song", order = 1)]
public class SongScriptable : ScriptableObject
{
    public AudioClip bgm;
    public float songDuration = 10;
    public float beatLength = 2.5f;
    public List<EnemySpawn> enemySpawns;

}
