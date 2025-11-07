using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class GridPlayer : MonoBehaviour
{
    public enum Facing { Up, Right, Down, Left }
    public Tilemap wallTilemap; // sleep hier je Wall Tilemap in
    public float gridSize = 1f;
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector3 direction;
    private Facing currentFacing;

    [SerializeField] private Facing startFacing = Facing.Up;

    private Quaternion startRotation;
    private Vector3 startDirection;

    void Awake()
    {

        ApplyFacing(startFacing);
        startRotation = transform.rotation;
        startDirection = direction;
    }

    public void ResetOrientation()
    {
        ApplyFacing(startFacing);
    }

    public void ResetToStart(Vector3 worldPos)
    {
        StopAllCoroutines();
        isMoving = false;

        transform.position = worldPos;

        // herstel richting exact zoals bij start
        direction = startDirection;
        transform.rotation = startRotation;
        currentFacing = startFacing;
    }

    public IEnumerator MoveSteps(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            Vector3 targetPos = transform.position + direction * gridSize;
            Vector3Int cellPos = wallTilemap.WorldToCell(targetPos);

            TileBase tile = wallTilemap.GetTile(cellPos);
            Debug.Log("TargetPos: " + targetPos + ", CellPos: " + cellPos + ", Tile: " + tile);

            // TEST LOG: bekijk welke cel wordt gecontroleerd
            Debug.Log("Checking cell: " + cellPos + ", Tile: " + wallTilemap.GetTile(cellPos));

            if (wallTilemap.GetTile(cellPos) != null)
            {
                // muur blokkeert beweging
                Debug.Log("Blocked by wall at " + cellPos);
                yield break;
            }

            yield return MoveOneStep(targetPos);
        }
    }

    private IEnumerator MoveOneStep(Vector3 endPos)
    {
        if (isMoving) yield break;
        isMoving = true;
        Vector3 startPos = transform.position;
        float elapsed = 0.2f;

        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = endPos;
        isMoving = false;
    }

    public void TurnLeft() 
    {
        currentFacing = (Facing)(((int)currentFacing + 3) % 4);
        ApplyFacing(currentFacing);
    }
    public void TurnRight() 
    {
        currentFacing = (Facing)(((int)currentFacing + 1) % 4);
        ApplyFacing(currentFacing);
    }

    public bool IsIdle() => !isMoving;

    private void ApplyFacing(Facing f)
    {
        currentFacing = f;
        direction = FacingToVector(f);
        // zet Z-rotatie netjes 0/90/180/270
        float z = FacingToAngle(f);
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    private static Vector3 FacingToVector(Facing f)
    {
        switch (f)
        {
            case Facing.Up: return Vector3.up;
            case Facing.Right: return Vector3.right;
            case Facing.Down: return Vector3.down;
            case Facing.Left: return Vector3.left;
        }
        return Vector3.up;
    }

    private static float FacingToAngle(Facing f)
    {
        switch (f)
        {
            case Facing.Up: return 0f;
            case Facing.Right: return -90f; // of 270, maar -90 is prima
            case Facing.Down: return 180f;
            case Facing.Left: return 90f;
        }
        return 0f;
    }
}
