using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CommandManager : MonoBehaviour
{
    public GridPlayer player;

    private List<string> commands = new List<string>();

    public TMP_InputField forwardInput;
    public TMP_InputField leftInput;
    public TMP_InputField rightInput;
    public TMP_InputField repeatInput;

    public Transform commandListContent;
    public GameObject commandLinePrefab;

    public Tilemap startTilemap;
    public Tilemap finishTilemap;

    public GameObject VictoryScreen;

    // --- COMMAND ADDERS ---

    public void AddForwardFromInput()
    {
        int steps = 1;
        if (forwardInput != null && !string.IsNullOrEmpty(forwardInput.text))
            int.TryParse(forwardInput.text, out steps);

        for (int i = 0; i < steps; i++)
            commands.Add("forward");

        AddCommandVisual("Forward x" + steps);
    }

    public void AddRightFromInput()
    {
        int turns = 1;
        if (rightInput != null && !string.IsNullOrEmpty(rightInput.text))
            int.TryParse(rightInput.text, out turns);

        for (int i = 0; i < turns; i++)
            commands.Add("right");

        AddCommandVisual("Right x" + turns);
    }

    public void AddLeftFromInput()
    {
        int turns = 1;
        if (leftInput != null && !string.IsNullOrEmpty(leftInput.text))
            int.TryParse(leftInput.text, out turns);

        for (int i = 0; i < turns; i++)
            commands.Add("left");

        AddCommandVisual("Left x" + turns);
    }

    public void AddRepeat()
    {
        int repeatCount = 1;
        if (!string.IsNullOrEmpty(repeatInput.text))
            int.TryParse(repeatInput.text, out repeatCount);

        commands.Add("repeat:" + repeatCount);
        AddCommandVisual("Repeat x" + repeatCount);
    }

    public void AddEndRepeat()
    {
        commands.Add("endrepeat");
        AddCommandVisual("End Repeat");
    }

    // --- EXECUTE COMMANDS ---

    public void ExecuteCommands()
    {
        StartCoroutine(RunCommands());
    }

    private IEnumerator RunCommands()
    {
        SetPlayerToStart();

        for (int i = 0; i < commands.Count; i++)
        {
            string cmd = commands[i];

            if (cmd.StartsWith("repeat:"))
            {
                int repeatCount = int.Parse(cmd.Split(':')[1]);
                int endIndex = commands.IndexOf("endrepeat", i + 1);
                var blockCommands = commands.GetRange(i + 1, endIndex - (i + 1));

                for (int r = 0; r < repeatCount; r++)
                {
                    foreach (string subCmd in blockCommands)
                    {
                        if (subCmd == "forward") yield return player.MoveSteps(1);
                        if (subCmd == "left") player.TurnLeft();
                        if (subCmd == "right") player.TurnRight();
                    }
                }
                i = endIndex;
            }
            else if (cmd == "forward") yield return player.MoveSteps(1);
            else if (cmd == "left") player.TurnLeft();
            else if (cmd == "right") player.TurnRight();
        }

        // FINISH CHECK
        Vector3Int playerCell = finishTilemap.WorldToCell(player.transform.position);
        TileBase tile = finishTilemap.GetTile(playerCell);

        if (tile != null)
        {
            VictoryScreen.SetActive(true);
            yield break;
        }
        else
        {
            SetPlayerToStart();
        }

        commands.Clear();
        foreach (Transform child in commandListContent)
            Destroy(child.gameObject);
    }

    // --- VISUAL LIST ---

    void AddCommandVisual(string text)
    {
        GameObject newLine = Instantiate(commandLinePrefab, commandListContent);
        newLine.GetComponent<TMP_Text>().text = text;
    }

    // --- PLAYER RESET ---

    void Start()
    {
        SetPlayerToStart();
    }

    void SetPlayerToStart()
    {
        BoundsInt bounds = startTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = startTilemap.GetTile(pos);
            if (tile != null)
            {
                Vector3 worldPos = startTilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f, 0);
                player.ResetToStart(worldPos);
                return;
            }
        }

        Debug.LogWarning("Geen start tile gevonden!");
    }

    public void ResetCommands()
    {
        commands.Clear();

        foreach (Transform child in commandListContent)
            Destroy(child.gameObject);

        SetPlayerToStart();
    }
}
