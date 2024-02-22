using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName="Song", menuName = "CustomData/Song", order = 1)]
public class SongScriptable : ScriptableObject
{
    public AudioClip bgm;
    public float songDuration = 10;
    public float beatLength = 2.5f;
    
    [Button]
    public void OrderByBeat()
    {
        // will order the list based on the beat c:
        enemySpawns = enemySpawns.OrderBy(e => e.beat).ToList();
    }
    
    public List<EnemySpawn> enemySpawns;
}
