using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Persistent Data", menuName = "New Player Persistent Data")]
public class PlayerPersistentData : ScriptableObject
{
    public PlayerPersistentCharacter[] characters;

/*this code will only exsists if we are working in the unity editor*/
#if UNITY_EDITOR 

    void OnValidate()
    {
        ResetCharacters();
    }

#endif

/*will reset data based on the PlayerPersistentCharacters script*/
    public void ResetCharacters() 
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].health = characters[i].characterPrefab.GetComponent<Character>().maxHp;
            characters[i].isDead = false;
        }
    } 
}