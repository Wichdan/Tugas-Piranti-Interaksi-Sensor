using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform _levelPart_Start;
    [SerializeField] private List<Transform> _easyLevelPartList;
    [SerializeField] private List<Transform> _mediumLevelPartList;
    [SerializeField] private Player _player;

    private enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        VeryHard,
    }

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = _levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (_player == null) return;
        if (Vector3.Distance(_player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        List<Transform> difficultyLevelPartList;

        switch (GetDifficulty())
        {
            default:
            case Difficulty.Easy: difficultyLevelPartList = _easyLevelPartList; break;
            case Difficulty.Medium: difficultyLevelPartList = _mediumLevelPartList; break;
            //case Difficulty.Hard: difficultyLevelPartList = _mediumLevelPartList; break;
            //case Difficulty.VeryHard: difficultyLevelPartList = _mediumLevelPartList; break;
        }

        Transform chosenLevelPart = difficultyLevelPartList[Random.Range(0, difficultyLevelPartList.Count)];

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private Difficulty GetDifficulty()
    {
        if(GameManager.Instance.LenghtCount >= 1) return Difficulty.Medium;
        //if(GameManager.Instance.LenghtCount >= 100) return Difficulty.Hard;
        //if(GameManager.Instance.LenghtCount >= 150) return Difficulty.VeryHard;
        return Difficulty.Easy;
    }
}
