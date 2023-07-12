using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*[] means an array*/
    public Character[] playerTeam;
    public Character[] enemyTeam;

    private List<Character> allCharacters = new List<Character>();

    [Header("Components")]
    public Transform[] playerTeamSpawns;
    public Transform[] enemyTeamSpawns;

    [Header("Data")]
    public PlayerPersistentData playerPersistentData;
    public CharacterSet defaultEnemySet;

    public static GameManager instance;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    } 

    void Start()
    {
        CreateCharacters(playerPersistentData, defaultEnemySet);
    }

    /*sending over playerData from PlayerPersistentData script and enemyTeamSet from CharacterSet script*/
    void CreateCharacters (PlayerPersistentData playerData, CharacterSet enemyTeamSet)
    {
        playerTeam = new Character[playerData.characters.Length];
        enemyTeam = new Character[enemyTeamSet.characters.Length];

        int playerSpawnIndex = 0;

        /*spawn player characters*/
        for(int i = 0; i < playerData.characters.Length; i++)
        {
            if(!playerData.characters[i].isDead)
            {
                Character character = CreateCharacter(playerData.characters[i].characterPrefab, playerTeamSpawns[playerSpawnIndex]);
                character.curHp = playerData.characters[i].health;
                playerTeam[i] = character;
                playerSpawnIndex++;
            }
            /*if player character is dead, will skip over them*/
            else
            {
                playerTeam[i] = null;
            }
        }

        /*spawn enemy characters*/
        for(int i = 0; i < enemyTeamSet.characters.Length; i++)
        {
            /*spawns in the enemy character*/
            Character character = CreateCharacter(enemyTeamSet.characters[i], enemyTeamSpawns[i]);
            /*assigns them to enemy team*/
            enemyTeam[i] = character;
        }

        /*adds playerTeam and enemyTeam arrays to allCharacters list*/
        allCharacters.AddRange(playerTeam);
        allCharacters.AddRange(enemyTeam);
    }

    Character CreateCharacter (GameObject characterPrefab, Transform spawnPos)
    {
        GameObject obj = Instantiate(characterPrefab, spawnPos.position, spawnPos.rotation);
        return obj.GetComponent<Character>();
    }
}
