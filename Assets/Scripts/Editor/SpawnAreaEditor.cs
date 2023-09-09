using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnArea))]
public class SpawnAreaEditor : Editor
{
    private void OnSceneGUI()
    {
        SpawnArea spawnArea = target as SpawnArea;

        GameObject player = spawnArea.GetPlayer();

        if(player != null)
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(player.transform.position, Vector3.up, spawnArea.SpawnDistanceToPlayer);
        }

        // Draw the spawn radius circle
        Handles.color = Color.red;
        Handles.DrawWireDisc(spawnArea.transform.position, Vector3.up, spawnArea.InnerRadiusOffset);

        // Draw the spawn radius circle
        Handles.color = Color.red;
        Handles.DrawWireDisc(spawnArea.transform.position, Vector3.up, spawnArea.SpawnRadius);

        // Draw the follow radius circle
        Handles.color = Color.blue;
        Handles.DrawWireDisc(spawnArea.transform.position, Vector3.up, spawnArea.StartSpawnRadius);
    }
}
