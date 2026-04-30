using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [Header("Generation")]
    public int totalRooms = 10;
    public float roomSpacing = 20f;

    [Header("Prefabs")]
    public GameObject roomPrefab;

    private HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

    private Vector2Int startPos = Vector2Int.zero;
    private Vector2Int bossRoomPos;

    void Start()
    {
        GenerateLayout();
        FindBossRoom();
        SpawnRooms();
    }

    void GenerateLayout()
    {
        roomPositions.Clear();
        roomPositions.Add(startPos);

        List<Vector2Int> availableRooms = new List<Vector2Int>();
        availableRooms.Add(startPos);

        while (roomPositions.Count < totalRooms && availableRooms.Count > 0)
        {
            Vector2Int current = availableRooms[Random.Range(0, availableRooms.Count)];

            List<Vector2Int> neighbors = GetNeighbors(current);
            Shuffle(neighbors);

            bool roomAdded = false;

            foreach (Vector2Int neighbor in neighbors)
            {
                if (roomPositions.Contains(neighbor))
                    continue;

                if (CountAdjacentRooms(neighbor) > 1)
                    continue;

                roomPositions.Add(neighbor);
                availableRooms.Add(neighbor);
                roomAdded = true;

                if (roomPositions.Count >= totalRooms)
                    break;
            }

            if (!roomAdded)
                availableRooms.Remove(current);
        }
    }

    public int CountAdjacentRooms(Vector2Int pos)
    {
        int count = 0;

        foreach (Vector2Int neighbor in GetNeighbors(pos))
        {
            if (roomPositions.Contains(neighbor))
                count++;
        }

        return count;
    }

    public void Shuffle(List<Vector2Int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector2Int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void FindBossRoom()
    {
        Dictionary<Vector2Int, int> distances = BFS(startPos);

        int maxDistance = -1;
        bossRoomPos = startPos;

        foreach (var pair in distances)
        {
            Vector2Int pos = pair.Key;
            int distance = pair.Value;

            int neighborCount = CountNeighbors(pos);

            if (distance > maxDistance && neighborCount == 1)
            {
                maxDistance = distance;
                bossRoomPos = pos;
            }
        }

        Debug.Log("Boss Room Position: " + bossRoomPos);
    }

    Dictionary<Vector2Int, int> BFS(Vector2Int start)
    {
        Dictionary<Vector2Int, int> distances = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        queue.Enqueue(start);
        distances[start] = 0;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            foreach (var neighbor in GetNeighbors(current))
            {
                if (roomPositions.Contains(neighbor) && !distances.ContainsKey(neighbor))
                {
                    distances[neighbor] = distances[current] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return distances;
    }

    void SpawnRooms()
    {
        foreach (Vector2Int pos in roomPositions)
        {
            Vector3 worldPos = new Vector3(
                pos.x * roomSpacing,
                pos.y * roomSpacing,
                0f
            );

            GameObject spawnedRoom = Instantiate(
                roomPrefab,
                worldPos,
                Quaternion.identity
            );

            spawnedRoom.name = "Room_" + pos.x + "_" + pos.y;

            if (pos == startPos)
            {
                Debug.Log("Start Room Spawned");
            }

            if (pos == bossRoomPos)
            {
                Debug.Log("Boss Room Spawned");
            }
        }
    }

    int CountNeighbors(Vector2Int pos)
    {
        int count = 0;

        foreach (var neighbor in GetNeighbors(pos))
        {
            if (roomPositions.Contains(neighbor))
                count++;
        }

        return count;
    }

    List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        return new List<Vector2Int>()
        {
            pos + Vector2Int.up,
            pos + Vector2Int.down,
            pos + Vector2Int.left,
            pos + Vector2Int.right
        };
    }
}