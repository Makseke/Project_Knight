using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private WorldSettings worldSettings;
    private Rigidbody2D player;
    public GameObject[] forestMonsters;


    void Start()
    {
        worldSettings = GameObject.FindGameObjectWithTag("World Settings").GetComponent<WorldSettings>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        if (transform.position.y - 11 > player.position.y && worldSettings.monsterCount < 5 && Random.Range(0,2) == 1)
        {
            if (worldSettings.biom == "forest")
            {
                int rand = Random.Range(0, forestMonsters.Length);
                Instantiate(forestMonsters[rand], transform.position, forestMonsters[rand].transform.rotation);
                Destroy(gameObject);
                worldSettings.monsterCount++;
            }
        }
    }
    void Update()
    {
        if (transform.position.y - 11 > player.position.y && worldSettings.monsterCount < 5)
        {
            if (worldSettings.biom == "forest")
            {
                int rand = Random.Range(0, forestMonsters.Length);
                Instantiate(forestMonsters[rand], transform.position, forestMonsters[rand].transform.rotation);
                Destroy(gameObject);
                worldSettings.monsterCount++;
            }
        }
        if (transform.position.y - 11 <= player.position.y)
        {
            Destroy(gameObject);
        }
    }
}
