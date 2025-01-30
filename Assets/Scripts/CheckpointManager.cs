using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject player;
    public Transform[] checkpoints;
    private int currentCheckpointNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(checkpoints.Length > 0)
        {
            player.transform.position = checkpoints[0].position;
        }
    }

    public void SetCheckpoint(int checkpointNum)
    {
        currentCheckpointNum = checkpointNum;
    }

    public void SpawnPlayerToCheckpoint()
    {
        player.transform.position = checkpoints[currentCheckpointNum].position;
    }
}
