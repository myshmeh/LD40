using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using Core;

public class RoomManager : MonoBehaviour, IObserver {
    [SerializeField]
    public GameObject floor;
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public GameObject wall;
    [SerializeField]
    public GameObject pc;
    [SerializeField]
    public GameObject banana;
    [SerializeField]
    public GameObject enemy;
    [SerializeField]
    public GameObject lawbook;
    public GameObject needle;

    [SerializeField]
    private string filePath;

    protected int enemyIndex;

    private void Awake()
    {
        //Screen.SetResolution(640, 480, false);

        //read map
        //string filePath = "Assets/Tiled/home.json";
        string jsonText = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "stage2.json"));
        var map = JSON.Parse(jsonText);

        enemyIndex = 0;

        //instantiate
        for(int i=0; i<4; i++)
        {
            InstantiateFromJsonData(map, i);
        }
    }

    protected void InstantiateFromJsonData(JSONNode jsonData, int layer)
    {
        for (int i = 0; i < jsonData["height"].AsInt; i++)
        {
            for (int j = 0; j < jsonData["width"].AsInt; j++)
            {
                //in the case of floor
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 4)
                    Instantiate(floor, new Vector2(j*jsonData["tilewidth"].AsInt, -i*jsonData["tilewidth"].AsInt), Quaternion.identity);
                //in the case of collision
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 1)
                    Instantiate(wall, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
                //in the case of player
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 5)
                    Instantiate(player, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
                //in the case of pc
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 3)
                    Instantiate(pc, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
                //in the case of banana
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 6)
                    Instantiate(banana, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
                //in the case of banana
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 2)
                {
                    var ins = Instantiate(enemy, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
                    ins.GetComponent<EnemyController>().id = enemyIndex++;
                }
                //in the case of banana
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 7)
                    Instantiate(lawbook, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
                //in the case of needle
                if (jsonData["layers"][layer]["data"][i * jsonData["width"].AsInt + j] == 8)
                    Instantiate(needle, new Vector2(j * jsonData["tilewidth"].AsInt, -i * jsonData["tilewidth"].AsInt), Quaternion.identity);
            }
        }
    }

    void IObserver.OnNotify(GameObject entity, EventMessage eventMsg, List<object> data)
    {
        ProcessOnNotify(entity, eventMsg, data);
    }

    protected virtual void ProcessOnNotify(GameObject entity, EventMessage eventMsg, List<object> data) { }

}
