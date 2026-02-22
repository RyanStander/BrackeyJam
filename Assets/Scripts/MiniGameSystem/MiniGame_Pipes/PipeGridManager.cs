using AudioManagement;
using PersistentManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeGridManager : BaseMinigame
{
    [Header("Grid Setup")]
    public List<Transform> pipeContainers;
    public Transform pipeContainerSelected;
    public int currentContainerIndex = 0;
    public int columns;
    public int rows;

    private Pipe[,] grid;
    private Vector2Int startPos;
    private bool startFound = false;

    public bool gameWon = false;

    void Start()
    {
        GenerateRandomPuzzleNumb();
        InitializeGrid();
        ScrambleBoard();
        CheckConnectivity();
        


    }

    public void GenerateRandomPuzzleNumb()
    {
        currentContainerIndex = Random.Range(0, pipeContainers.Count);
        pipeContainerSelected = pipeContainers[currentContainerIndex];
        pipeContainerSelected.gameObject.SetActive(true);
    }

    private void ScrambleBoard()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Pipe pipe = grid[x, y];
                if (pipe != null)
                {
                    pipe.RandomizeRotation();
                }
            }
        }
    }

    void InitializeGrid()
    {
        if (pipeContainerSelected == null)
        {
            Debug.LogError("You forgot to assign the container that holds pipes");
            return;
        }

        grid = new Pipe[columns, rows];

        if (pipeContainerSelected.childCount != columns * rows)
        {
            Debug.LogError("make sure you have 36 total pipe tiles in the grid");
            return;
        }

        for (int i = 0; i < pipeContainerSelected.childCount; i++)
        {
            int x = i % columns;
            int y = i / columns;

            Pipe pipe = pipeContainerSelected.GetChild(i).GetComponent<Pipe>();

            if (pipe == null)
            {
                Debug.LogError("missing pipe scirpt");
                continue;
            }
            grid[x, y] = pipe;

            if (pipe.isStartNode)
            {
                startPos = new Vector2Int(x, y);
                startFound = true;
            }
        }

        if (!startFound)
        {
            Debug.LogError("no start pipe found");
        }
    }

    public void CheckConnectivity()
    {
        if (!startFound || grid == null) return;

        Debug.Log($"running vadityity check at [{startPos.x}, {startPos.y}]");

        Queue<Vector2Int> queue = new Queue<Vector2Int>(); //today we learned about queueing 
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>(); //today we also learned about hashsets which are like lists but they dont allow duplicates and theyre faster for checking if something is in them

        queue.Enqueue(startPos);
        visited.Add(startPos);

        bool reachedEnd = false;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            Pipe currentPipe = grid[current.x, current.y];

            if (currentPipe == null) continue;

            //Debug.Log($"checking pipe at [{current.x}, {current.y}]...");

            if (currentPipe.isEndNode)
            {
                Debug.Log($"WIN! reached end node at [{current.x}, {current.y}]!");
                reachedEnd = true;
                break;
            }

            if (currentPipe.up && current.y > 0)
            {
                Vector2Int next = new Vector2Int(current.x, current.y - 1);
                Pipe nextPipe = grid[next.x, next.y];

                if (nextPipe != null && !visited.Contains(next))
                {
                    if (nextPipe.down)
                    {
                        Debug.Log($"connected UP to [{next.x}, {next.y}]");
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                    else
                    {
                        Debug.Log($"BLOCKED going UP to pipe at [{next.x}, {next.y}] has no DOWN connection.");
                    }
                }
            }

            if (currentPipe.down && current.y < rows - 1)
            {
                Vector2Int next = new Vector2Int(current.x, current.y + 1);
                Pipe nextPipe = grid[next.x, next.y];

                if (nextPipe != null && !visited.Contains(next))
                {
                    if (nextPipe.up)
                    {
                        Debug.Log($"Connected DOWN to [{next.x}, {next.y}]");
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                    else
                    {
                        Debug.Log($"BLOCKED going DOWN to pipe at [{next.x}, {next.y}] has no UP connection.");
                    }
                }
            }

            if (currentPipe.right && current.x < columns - 1)
            {
                Vector2Int next = new Vector2Int(current.x + 1, current.y);
                Pipe nextPipe = grid[next.x, next.y];

                if (nextPipe != null && !visited.Contains(next))
                {
                    if (nextPipe.left)
                    {
                        Debug.Log($"Connected RIGHT to [{next.x}, {next.y}]");
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                    else
                    {
                        Debug.Log($"BLOCKED going RIGHT to pipe at [{next.x}, {next.y}] has no LEFT connection.");
                    }
                }
            }

            if (currentPipe.left && current.x > 0)
            {
                Vector2Int next = new Vector2Int(current.x - 1, current.y);
                Pipe nextPipe = grid[next.x, next.y];

                if (nextPipe != null && !visited.Contains(next))
                {
                    if (nextPipe.right)
                    {
                        Debug.Log($"Connected LEFT to [{next.x}, {next.y}]");
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                    else
                    {
                        Debug.Log($"BLOCKED going LEFT to pipe at [{next.x}, {next.y}] has no RIGHT connection.");
                    }
                }
            }
        }

        if (reachedEnd)
        {
            foreach (Vector2Int pos in visited)
            {
                Pipe pipe = grid[pos.x, pos.y];
                if (pipe != null)
                {
                    gameWon = true;
                    pipe.GetComponent<Image>().color = new Color32(26, 255, 0, 255);
                }
            }
            AudioManager.PlayOneShot(AudioDataHandler.MinigamePipes.PipesDone());
            StartCoroutine(WinSequence());
            Debug.Log("YOU WIN! The pipes are connected.");
        }
        else
        {
            Debug.Log("PATH FAILED. Could not reach the end node.");
        }
    }

    IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(2f);
        FinishGame(true);
    }
}