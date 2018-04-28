using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GPE338Final.Units;
using GPE338Final.Projectiles;
using GPE338Final.CustomEvents;

public class GameManger : MonoBehaviour {

    public static GameManger instance;
    
    public List<Unit> playerUnitPrefabs;
    public List<Unit> enemyUnitPrefabs;
    public List<Projectile> projectilePrefabs;

    public UnitSpawn playerSpawn;
    public UnitSpawn enemySpawn;

    public string UNIT_DEATH;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        LoadResources();
        UNIT_DEATH = "UNIT_DEATH";
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SpawnPlayerUnit(UnitType type, UnitRace race)
    {
        List<Unit> temp = (from t in playerUnitPrefabs where t.data.Type == type && t.data.Race == race select t).ToList<Unit>();

        Unit unitPref = null;

        if (temp.Count > 0)
        {
            unitPref = temp[0];
        }
        
        if (unitPref != null)
        {
            ObjectPoolManager.instance.GetObject(unitPref.gameObject, playerSpawn.tf.position, playerSpawn.tf.rotation);
        }
    }

    public void SpawnEnemyUnit(UnitType type, UnitRace race)
    {
        List<Unit> temp = (from t in enemyUnitPrefabs where t.data.Type == type && t.data.Race == race select t).ToList<Unit>();

        Unit unitPref = null;

        if (temp.Count > 0)
            unitPref = temp[0];

        if(unitPref != null)
        {
            ObjectPoolManager.instance.GetObject(unitPref.gameObject, enemySpawn.tf.position, enemySpawn.tf.rotation);
        }

    }

    void LoadResources()
    {
        Unit[] tempPlayerUnits = Resources.LoadAll<Unit>("PlayerUnits");
        playerUnitPrefabs = new List<Unit>(tempPlayerUnits);

        Unit[] tempEnemyUnits = Resources.LoadAll<Unit>("EnemyUnits");
        enemyUnitPrefabs = new List<Unit>(tempEnemyUnits);

        Projectile[] tempProjectiles = Resources.LoadAll<Projectile>("Projectiles");
        projectilePrefabs = new List<Projectile>(tempProjectiles);
    }
}
