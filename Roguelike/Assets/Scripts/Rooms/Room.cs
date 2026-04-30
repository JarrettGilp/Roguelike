using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class Room : ScriptableObject
{
    public int roomID;

    public bool doorUp;
    public bool doorDown;
    public bool doorLeft;
    public bool doorRight;

    public bool isStartRoom;
    public bool isShop;
    public bool isBossDoorRoom;
    public bool isBossRoom;

    public int DoorCount()
    {
        int count = 0;

        if (doorUp) count++;
        if (doorDown) count++;
        if (doorLeft) count++;
        if (doorRight) count++;

        return count;
    }
}