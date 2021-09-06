using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChecker : MonoBehaviour
{
    public Transform[] spawnPositions;
    public Transform basePosition;


    public bool PathCheck()
    {
        GraphNode spawnNode;
        GraphNode baseNode = AstarPath.active.GetNearest(basePosition.position, NNConstraint.Default).node;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            spawnNode = AstarPath.active.GetNearest(spawnPositions[i].position, NNConstraint.Default).node;
            if (!PathUtilities.IsPathPossible(spawnNode, baseNode))
            {
                return false;
            }
        }

        return true;
    }
}
