using UnityEngine;

public class MazeGeneratorController : MonoBehaviour
{
    public int mazeWidth = 5;
    public int mazeHeight = 5;

    MazeCell[,] generatedMaze;
    public GameObject[] mazeParts = new GameObject[6];
    //0, 1, 2 corner, 2 straight, 3, 4

	// Use this for initialization
	void Start ()
    {
        GenerateMaze();
        CreateMaze();
    }

    void GenerateMaze()
    {
        generatedMaze = new MazeCell[mazeWidth, mazeHeight];
        //starts at bottom left like the book does
        for (int j = mazeHeight - 1; j >= 0; j--)
        {
            for (int i = 0; i < mazeWidth; i++)
            {
                generatedMaze[i, j] = new MazeCell();
                int coinResult = coinFlip();
                if(i == mazeWidth-1)
                {
                    generatedMaze[i, j].doorUp = 1;
                }
                else if(j == 0)
                {
                    generatedMaze[i, j].doorRight = 1;
                }
                else
                {
                    if (coinResult == 1)
                    {
                        //carve north
                        generatedMaze[i, j].doorUp = 1;
                    }
                    else
                    {
                        //carve east
                        generatedMaze[i, j].doorRight = 1;
                    }
                }
            }
        }
    }

    void CreateMaze()
    {
        float x = 0.0f, z = 0.0f;
        for(int i = 0; i < mazeWidth; i++)
        {
            for(int j = 0; j < mazeHeight; j++)
            {
                string doorBinary = generatedMaze[i, j].doorUp.ToString() + generatedMaze[i, j].doorRight.ToString();
                
                if (j == mazeHeight - 1)
                {
                    doorBinary = doorBinary + "0";
                }
                else
                {
                    doorBinary = doorBinary + generatedMaze[i, j + 1].doorUp.ToString();
                }

                if (i < mazeWidth - 1)
                {
                    doorBinary = doorBinary + "0";
                }
                else
                {
                    doorBinary = doorBinary + generatedMaze[i - i, j].doorRight.ToString();
                }

                //translated in steps of 6
                switch(doorBinary)
                {
                    case "0000":
                        Instantiate(mazeParts[0], new Vector3(x, 0, z), Quaternion.identity);
                        Debug.Log("No Doors");
                        break;
                    case "0001":
                        Instantiate(mazeParts[1], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 270, 0)));
                        Debug.Log("1 Door: Left");
                        break;
                    case "0010":
                        Instantiate(mazeParts[1], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 180, 0)));
                        Debug.Log("1 Door: Down");
                        break;
                    case "0011":
                        Instantiate(mazeParts[2], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 270, 0)));
                        Debug.Log("2 Doors: Down, Left");
                        break;
                    case "0100":
                        Instantiate(mazeParts[1], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 90, 0)));
                        Debug.Log("1 Door: Right");
                        break;
                    case "0101":
                        Instantiate(mazeParts[3], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 90, 0)));
                        Debug.Log("2 Doors: Right, Left");
                        break;
                    case "0110":
                        Instantiate(mazeParts[2], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 90, 0)));
                        Debug.Log("2 Doors: Right, Down");
                        break;
                    case "0111":
                        Instantiate(mazeParts[4], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 180, 0)));
                        Debug.Log("3 Doors: Right, Down, Left");
                        break;
                    case "1000":
                        Instantiate(mazeParts[1], new Vector3(x, 0, z), Quaternion.identity);
                        Debug.Log("1 Door: Up");
                        break;
                    case "1001":
                        Instantiate(mazeParts[2], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 270, 0)));
                        Debug.Log("2 Doors: Up, Left");
                        break;
                    case "1010":
                        Instantiate(mazeParts[3], new Vector3(x, 0, z), Quaternion.identity);
                        Debug.Log("2 Doors: Up, Down");
                        break;
                    case "1011":
                        Instantiate(mazeParts[4], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 270, 0)));
                        Debug.Log("3 Doors: Up, Down, Left");
                        break;
                    case "1100":
                        Instantiate(mazeParts[2], new Vector3(x, 0, z), Quaternion.identity);
                        Debug.Log("2 Doors: Up, Right");
                        break;
                    case "1101":
                        Instantiate(mazeParts[4], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 0, 0)));
                        Debug.Log("3 Doors: Up, Right, Left");
                        break;
                    case "1110":
                        Instantiate(mazeParts[4], new Vector3(x, 0, z), Quaternion.Euler(new Vector3(0, 90, 0)));
                        Debug.Log("3 Doors: Up, Right, Down");
                        break;
                    case "1111":
                        Instantiate(mazeParts[5], new Vector3(x, 0, z), Quaternion.identity);
                        Debug.Log("4 Doors: Up, Right, Down, Left");
                        break;
                    default:
                        break;
                }
                z += 6.0f;
            }
            x += 6.0f;
            z = 0.0f;
        }
    }

    int coinFlip()
    {
        return UnityEngine.Random.Range(0, 2);
    }
}

public class MazeCell
{
    public int doorUp = 0, doorRight = 0;
}