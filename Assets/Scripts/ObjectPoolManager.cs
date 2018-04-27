using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPE338Final.Units;
using GPE338Final.Projectiles;

public class ObjectPoolManager : MonoBehaviour {

    public static ObjectPoolManager instance;

    public List<GameObject> objectPool;

    public int numberOfPlayerUnits;
    public int numberOfEnemyUnits;
    public int numberOfProjectiles;

    private void Awake()
    {
        if (instance == null)
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
        InitPools();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitPools()
    {
        objectPool = new List<GameObject>();

        for(int i = 0; i < (numberOfPlayerUnits * GameManger.instance.playerUnitPrefabs.Count); i++)
        {
            foreach(Unit u in GameManger.instance.playerUnitPrefabs)
            {
                GameObject temp = Instantiate(u.gameObject, transform, true);
                temp.name = "Player_" + u.data.Race.ToString() + "_" + u.data.Type.ToString();
                temp.transform.position = this.transform.position;
                temp.tag = "Player";
                temp.layer = 8;
                temp.SetActive(false);
                objectPool.Add(temp);
            }
        }

        for(int i = 0; i < (numberOfEnemyUnits * GameManger.instance.enemyUnitPrefabs.Count); i++)
        {
            foreach(Unit e in GameManger.instance.enemyUnitPrefabs)
            {
                GameObject temp = Instantiate(e.gameObject, transform, true);
                temp.name = "Enemy_" + e.data.Race.ToString() + "_" + e.data.Type.ToString();
                temp.transform.position = this.transform.position;
                temp.tag = "Enemy";
                temp.layer = 9;
                temp.SetActive(false);
                objectPool.Add(temp);
            }
        }

        for(int i = 0; i < (numberOfProjectiles * GameManger.instance.projectilePrefabs.Count); i++)
        {
            foreach(Projectile p in GameManger.instance.projectilePrefabs)
            {
                GameObject temp = Instantiate(p.gameObject, transform, true);
                temp.name = p.gameObject.name;
                temp.transform.position = this.transform.position;
                temp.SetActive(false);
                objectPool.Add(temp);
            }
        }
    }
    
    public GameObject GetObject(GameObject prefab, Vector2 position, Quaternion rotation)
    {
        foreach(GameObject obj in objectPool)
        {
            if(!obj.activeInHierarchy)
            {
                if(obj.name == prefab.name)
                {
                    obj.name = prefab.name;
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;

                    obj.SetActive(true);

                    return obj;
                }
            }
        }

        GameObject newObject = Instantiate(prefab, this.transform, true);
        newObject.name = prefab.name;
        newObject.transform.position = position;
        newObject.transform.rotation = rotation;

        newObject.SetActive(true);

        objectPool.Add(newObject);

        return newObject;
    }
}
