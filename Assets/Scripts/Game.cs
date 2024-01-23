using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("white_rook", 0, 0),
            Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0),
            Create("white_queen", 3, 0),
            Create("white_king", 4, 0),
            Create("white_bishop", 5, 0),
            Create("white_knight", 6, 0),
            Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1),
            Create("white_pawn", 1, 1),
            Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1),
            Create("white_pawn", 4, 1),
            Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1),
            Create("white_pawn", 7, 1),
        };
        playerBlack = new GameObject[]
        {
            Create("black_rook", 0, 7),
            Create("black_knight", 1, 7),
            Create("black_bishop", 2, 7),
            Create("black_queen", 3, 7),
            Create("black_king", 4, 7),
            Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7),
            Create("black_rook", 7, 7),
            Create("black_pawn", 0, 6),
            Create("black_pawn", 1, 6),
            Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6),
            Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6),
            Create("black_pawn", 7, 6),
        };
        for (int i = 0; i < 16; ++i)
        {
            setPosition(playerBlack[i]);
            setPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string sprite_name, int xBoard, int yBoard)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -3), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = sprite_name;
        cm.setXBoard(xBoard);
        cm.setYBoard(yBoard);
        cm.Activate();
        return obj;
    }
    public string getCurrentPlayer()
    {
        return this.currentPlayer;
    }

    public void setPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.getXBoard(), cm.getYBoard()] = obj;
    }

    public void setPositionEmpty(int xBoard, int yBoard)
    {
        positions[xBoard, yBoard] = null;
    }

    public GameObject getPosition(int xBoard, int yBoard)
    {
        return positions[xBoard, yBoard];
    }

    public bool positionOnBoard(int xBoard, int yBoard)
    {
        return xBoard >= 0 && xBoard < positions.GetLength(0) 
            && yBoard >= 0 && yBoard < positions.GetLength(1);
    }

    public bool isGameOver()
    {
        return this.gameOver;
    }

    public void nextTurn()
    {
        if (this.currentPlayer == "white")
        {
            this.currentPlayer = "black";
        }
        else
        {
            this.currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }

    }

    public void Winner(string playerWin)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWin + " is Winner!";


        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
