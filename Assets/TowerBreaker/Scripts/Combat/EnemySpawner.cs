using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase normalEnemyPrefab;
    [SerializeField] private EnemyBase speedElitePrefab;
    [SerializeField] private EnemyBase hpElitePrefab;

    public EnemyBase SpawnNormalEnemy(Transform spawnPoint)
    {
        return Instantiate(normalEnemyPrefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }

    public EnemyBase SpawnEliteEnemy(EliteEnemyType type, Transform spawnPoint)
    {
        EnemyBase prefab = null;

        switch (type)
        {
            case EliteEnemyType.SpeedElite:
                prefab = speedElitePrefab;
                break;

            case EliteEnemyType.HpElite:
                prefab = hpElitePrefab;
                break;
        }

        return Instantiate(prefab, spawnPoint.position, Quaternion.identity, spawnPoint);
    }
}
