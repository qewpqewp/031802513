using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapCreator : MonoBehaviour
{

    public MapSprite Sprites;
    public int enemynumber;
    public Tilemap Wallmap;
    public Tilemap Floormap;
    public List<Tile> Tiles;
    //大地图宽高
    public int levelW = 10;
    public int levelH = 10;
    // Start is called before the first frame update
    void Start()
    {
        levelH = PlayerPrefs.GetInt("height");
        levelW = PlayerPrefs.GetInt("width");
        enemynumber = PlayerPrefs.GetInt("count");
        Tiles = new List<Tile>();
        InitTile();
   
        InitData();
        spawnenemy();
    }
    void spawnenemy()
    {
        
        GameObject enemy = Resources.Load<GameObject>("enemy/enemy1");
        for (int i = 0; i < enemynumber; i++)
        {

            GameObject temp = (GameObject)Instantiate(enemy);
            temp.transform.position = Wallmap.transform.position + new Vector3(Random.Range(0, levelW/2)+3f, Random.Range(0, levelH/2)+3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitData()
    {

        
        for (int i = 0; i < levelH; i++)
        {//根据地面类型TileType初始化tilemap
            for (int j = 0; j < levelW; j++)
            { 
                if(i==0 || j==0 || i==levelH-1 || j == levelW - 1)
                {
                    
                    Wallmap.SetTile(new Vector3Int(j, i, 0), Tiles[getnum(i,j)]);
                }
                Floormap.SetTile(new Vector3Int(j, i, 0), Tiles[8]);
            }
        }
    }

    public int getnum(int i,int j)
    {
        if(i == levelH - 1)
        {
            if (j == 0)
            {
                return 0;
            }else if(j== levelW - 1)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        else if (i == 0)
        {
            if (j == 0)
            {
                return 5;
            }
            else if (j == levelW - 1)
            {
                return 7;
            }
            else
            {
                return 6;
            }
        }
        else
        {
            if (j == 0)
            {
                return 3;
            }
            else if (j == levelW - 1)
            {
                return 4;
            }

        }
        return -1;
    }
    void InitTile()
    {
        
        for(int i = 0; i < 8; i++)
        {
            Tile temp = ScriptableObject.CreateInstance<Tile>();

            temp.sprite = Sprites.Wallsprite[i];
            Tiles.Add(temp);
        }

        Tile temp1 = ScriptableObject.CreateInstance<Tile>();
        temp1.sprite = Sprites.Floorsprite;
        Tiles.Add(temp1);




    }


}



[System.Serializable]
public struct MapSprite
{
   
    public List<Sprite> Wallsprite;
    public Sprite Floorsprite;


}