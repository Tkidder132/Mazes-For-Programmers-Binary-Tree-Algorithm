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
        InitializeMaze();
        GenerateMaze();
        CreateMaze();
    }

    void InitializeMaze()
    {
        generatedMaze = new MazeCell[mazeWidth, mazeHeight];
        for (int i = 0; i < mazeWidth; i++)
        {
            for(int j = 0; j < mazeHeight; j++)
            {
                generatedMaze[i, j] = new MazeCell();
            }
        }
    }

    void GenerateMaze()
    {
        //starts at bottom left like the book does
        for (int y = mazeHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < mazeWidth; x++)
            {
                if(x == mazeWidth - 1)
                {
                    if(y > 0)
                    {
                        generatedMaze[y, x].doorUp = 1;
                        generatedMaze[y - 1, x].doorDown = 1;
                    }
                }
                else if(y == 0)
                {
                    generatedMaze[y, x].doorRight = 1;
                    if(x < mazeWidth - 1)
                    {
                        generatedMaze[y, x + 1].doorLeft = 1;
                    }
                }
                else
                {
                    int coinResult = coinFlip();
                    if (coinResult == 1)
                    {
                        //carve north
                        if (y > 0)
                        {
                            generatedMaze[y, x].doorUp = 1;
                            generatedMaze[y - 1, x].doorDown = 1;
                        }
                    }
                    else
                    {
                        //carve east
                        generatedMaze[y, x].doorRight = 1;
                        if (x < mazeWidth - 1)
                        {
                            generatedMaze[y, x + 1].doorLeft = 1;
                        }
                    }
                }
            }
        }
    }

    void CreateMaze()
    {
        float xCoord = 0.0f, zCoord = 0.0f;
        for (int y = mazeHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < mazeWidth; x++)
            {
                string doorBinary = generatedMaze[y, x].doorUp.ToString() + generatedMaze[y, x].doorRight.ToString() +
                                    generatedMaze[y, x].doorDown.ToString() + generatedMaze[y, x].doorLeft.ToString();

                Vector3 location = new Vector3(xCoord, 0, zCoord);
                GameObject mazePiece;
                //translated in steps of 6
                switch (doorBinary)
                {
                    case "0000":
                        mazePiece = Instantiate(mazeParts[0], location, Quaternion.identity) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                    mazePiece.transform.SetParent(transform);
                        break;
                    case "0001":
                        mazePiece = Instantiate(mazeParts[1], location, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "0010":
                        mazePiece = Instantiate(mazeParts[1], location, Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "0011":
                        mazePiece = Instantiate(mazeParts[2], location, Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "0100":
                        mazePiece = Instantiate(mazeParts[1], location, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "0101":
                        mazePiece = Instantiate(mazeParts[3], location, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "0110":
                        mazePiece = Instantiate(mazeParts[2], location, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "0111":
                        mazePiece = Instantiate(mazeParts[4], location, Quaternion.Euler(new Vector3(0, 180, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1000":
                        mazePiece = Instantiate(mazeParts[1], location, Quaternion.identity) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1001":
                        mazePiece = Instantiate(mazeParts[2], location, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1010":
                        mazePiece = Instantiate(mazeParts[3], location, Quaternion.identity) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1011":
                        mazePiece = Instantiate(mazeParts[4], location, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1100":
                        mazePiece = Instantiate(mazeParts[2], location, Quaternion.identity) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1101":
                        mazePiece = Instantiate(mazeParts[4], location, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1110":
                        mazePiece = Instantiate(mazeParts[4], location, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    case "1111":
                        mazePiece = Instantiate(mazeParts[5], location, Quaternion.identity) as GameObject;
                        mazePiece.name = "[" + x + ", " + y + "]";
                        mazePiece.transform.SetParent(transform);
                        break;
                    default:
                        break;
                }

                xCoord += 6.0f;
            }
            zCoord += 6.0f;
            xCoord = 0.0f;
        }
    }

    int coinFlip()
    {
        return UnityEngine.Random.Range(0, 2);
    }
}

public class MazeCell
{
    public int doorUp = 0, doorRight = 0, doorLeft = 0, doorDown = 0;
}