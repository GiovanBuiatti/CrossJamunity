using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] Transform[] SpawnContainer;

    public GameObject[] tilePrefabs;
    

    private Transform playerTransform;
    
    private float spawnZ = 100.0f;
    //Cette variable sert a faire spawn en avance des prefabs
    private float tileLength = 12.0f;
    //Cette variable est le nombre de tile en jeu 
    private int amnTilesOnScreen = 10;
    //Cette variable sert a ce que les tiles ne se suppriment pas trop tot
    private float safeZone = 15.0f;

    private int lastPrefabIndex = 0;
    
    private List<GameObject> activeTiles;

    

    // Start is called before the first frame update
    private void Start()
    {

        

        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

       for(int i = 0; i < amnTilesOnScreen; i++)
        {
            if( i < 2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
            
        }

    }

    // Update is called once per frame
    private void Update()
    {
        //Ce if veut dire : quand on passe un certain point, une nouvelle Tile apparait (principe du niveau infini) / Il sert aussi a supprimer les tiles une fois d�pass�s
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }


    //Ici on a la fonction qui fait spawn des PREFABS ALEATOIREMENT
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go = null;


        
        if(prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;

        }
        
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }
        
        
        int randIndex = Random.Range(0, 3);
        Debug.Log("index :" + randIndex);



        go.transform.SetParent(transform);


        //go.transform.position = Vector3.forward * spawnZ;
        go.transform.position = new Vector3(
            this.transform.position.x + SpawnContainer[randIndex].transform.position.x,
            this.transform.position.y,
            (Vector3.forward * spawnZ).magnitude) ;

        

        //go.transform.position = new Vector3(SpawnContainer[randIndex].transform.position.x, SpawnContainer[randIndex].transform.position.y, this.transform.position.z);

        spawnZ += tileLength;

        activeTiles.Add(go);

    }

    //Ici on supprime les prefabs qui sont derriere le joueur 
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    //Cette fonction est celle qui permet le spawn de tile d'�tre al�atoire
    private int RandomPrefabIndex()
    {
        if(tilePrefabs.Length <= 1)
        
        return 0;
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
        
    }
}
