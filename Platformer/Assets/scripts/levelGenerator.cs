using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public int mapWidth = 50;

    public TileBase ground;
    public TileBase grass;

    public Transform endObject;
    public Transform bottomObject;

    public GameObject staticEnemy;
    public GameObject moverEnemy;
    public GameObject smartEnemy;

    private const int firstTileX = 5;
    void Start()
    {
        generateMap();
        generateEnemies();
    }

    void createGround()
    {
        for (int idx = 0; idx < mapWidth; ++idx)
        {
            Vector3Int p = new Vector3Int(idx + firstTileX, -3);
            tilemap.SetTile(p, grass);
        }
    }

    void createHoles()
    {
        int holeCount = Random.Range(1, mapWidth / 10 + 1);
        int holeFirst = Random.Range(5, 8);
        for (int hole = 0; hole < holeCount; ++hole)
        {
            int width = Random.Range(1, 5);

            for (int x = 0; x < width; ++x)
            {
                Vector3Int p = new Vector3Int(holeFirst + x + firstTileX, -3);
                tilemap.SetTile(p, null);
            }

            //if (width >= 4)
            //{
            //    tilemap.SetTile(new Vector3Int(holeFirst + 2 + firstTileX, 0), grass);
            //    tilemap.SetTile(new Vector3Int(holeFirst + 3 + firstTileX, 0), grass);
            //}

            holeFirst += Random.Range(mapWidth / holeCount - 2, mapWidth / holeCount + 2);

        }
    }

    void createFloatingIslands()
    {
        int islandCount = Random.Range (1, mapWidth / 15 + 1);

        int firstTile = Random.Range(10, 18);

        for(int island = 0; island < islandCount; ++island)
        {
            bool stair = Random.Range(1, 3) == 1;

            if(stair)
            {
                int count = Random.Range(2, 3);
                int height = 0;

                int x = 0;
                int y = 0;
                for (int c = 0; c < count; ++c)
                {
                    tilemap.SetTile(new Vector3Int(firstTile + x, height + y), grass);
                    ++x;
                    tilemap.SetTile(new Vector3Int(firstTile + x, height + y), grass);
                    ++x; y += 2;
                }

                firstTile += count * 2 + Random.Range(12, 17);
            }
            else
            {
                int height = Random.Range(-1, 1);
                int wifth = Random.Range(1, 5);

                for (int x = 0; x < wifth; ++x)
                    tilemap.SetTile(new Vector3Int(firstTile + x, height), grass);

                firstTile += wifth + Random.Range(mapWidth / islandCount - 2, mapWidth/islandCount + 2);
            }
        }
    }

    void generateMap()
    {
        bottomObject.localScale = new Vector3(mapWidth * 2.5f, 1);

        createGround();
        createHoles();
        createFloatingIslands();
        
        tilemap.tag = "Ground";
        endObject.position = new Vector3(mapWidth + firstTileX - 2, -0.5f);
    }

    void generateEnemies()
    {
        float posX = 6;
        staticEnemy.transform.position = new(posX, -1.9f);
        GameObject one = GameObject.Instantiate(staticEnemy);
        one.transform.position = new(posX + 0.5f, -1.9f);
        GameObject two = GameObject.Instantiate(staticEnemy);
        two.transform.position = new(posX + 1, -1.9f);


        smartEnemy.transform.position = new (mapWidth / 2, 3);

        //GameObject newMover = Instantiate(moverEnemy);
    }
}
