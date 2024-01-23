using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    [Header("Preferences")]
    public GameObject controller;
    public GameObject movePlate;

    //position on board
    private int xBoard = -1;
    private int yBoard = -1;

    // track 'black' or 'white';
    private string player;

    [Header("Black")]
    public Sprite black_king, black_queen, black_bishop, black_knignt, black_rook, black_pawn;

    [Header("White")]
    public Sprite white_king, white_queen, white_bishop, white_knignt, white_rook, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                this.player = "black";
                break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                this.player = "black";
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knignt;
                this.player = "black";
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                this.player = "black";
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                this.player = "black";
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                this.player = "black";
                break;
            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                this.player = "white";
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                this.player = "white";
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knignt;
                this.player = "white";
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                this.player = "white";
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                this.player = "white";
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                this.player = "white";
                break;
        }

    }

    public Vector2 convertCoors(int xMatrix, int yMatrix)
    {
        float x = ((xMatrix - 4) * 0.22f + 0.11f) * 5;
        float y = ((yMatrix - 4) * 0.22f + 0.11f) * 5;
        return new Vector2(x, y);
    }

    public void SetCoords()
    {
        Vector2 coord = convertCoors(xBoard, yBoard);

        this.transform.position = new Vector3(coord.x, coord.y, -1);
    }

    public int getXBoard()
    {
        return xBoard;
    }

    public int getYBoard()
    {
        return yBoard;
    }

    public void setXBoard(int x)
    {
        xBoard = x;
    }

    public void setYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().isGameOver() && controller.GetComponent<Game>().getCurrentPlayer() == this.player)
        {
            destroyMovePlate();

            initiateMovePlate();
        }
    }

    public void destroyMovePlate()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; ++i)
        {
            Destroy(movePlates[i]);
        }
    }

    public void initiateMovePlate()
    {
        switch (this.name)
        {
            case "black_king":
            case "white_king":
                surroundMovePlate();
                break;
            case "black_queen":
            case "white_queen":
                lineMovePlate(1, 0);
                lineMovePlate(1, 1);
                lineMovePlate(0, 1);
                lineMovePlate(-1, 1);
                lineMovePlate(-1, 0);
                lineMovePlate(-1, -1);
                lineMovePlate(0, -1);
                lineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                shapeLMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                lineMovePlate(1, 1);
                lineMovePlate(-1, 1);
                lineMovePlate(-1, -1);
                lineMovePlate(1, -1);
                break;
            case "black_rook":
            case "white_rook":
                lineMovePlate(0, 1);
                lineMovePlate(1, 0);
                lineMovePlate(0, -1);
                lineMovePlate(-1, 0);
                break;
            case "black_pawn":
                pawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                pawnMovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void movePlateSpawn(int xMatrix, int yMatrix)
    {
        Vector2 coord = convertCoors(xMatrix, yMatrix);

        GameObject mp = Instantiate(movePlate, new Vector3(coord.x, coord.y, -2), Quaternion.identity);
    
        MovePlate mpScript = mp.GetComponent<MovePlate>();

        mpScript.setReference(gameObject);
        mpScript.setCoords(xMatrix, yMatrix);
    }

    public void movePlateAttackSpawn(int xMatrix, int yMatrix)
    {
        Vector2 coord = convertCoors(xMatrix, yMatrix);

        GameObject mp = Instantiate(movePlate, new Vector3(coord.x, coord.y, -2), Quaternion.identity);

        mp.GetComponent<SpriteRenderer>().color = Color.red;

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.setReference(gameObject);
        mpScript.setCoords(xMatrix, yMatrix);
    }

    public void lineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.positionOnBoard(x, y) && sc.getPosition(x, y) == null)
        {
            movePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        if (sc.positionOnBoard(x, y) && sc.getPosition(x, y).GetComponent<Chessman>().player != this.player ) 
        {
            movePlateAttackSpawn(x, y);    
        }
    }
       
    public void pointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.getPosition(x, y);
            if (cp == null)
            {
                movePlateSpawn(x, y);
            } 
            else if (this.player != cp.GetComponent<Chessman>().player)
            {
                movePlateAttackSpawn(x, y); 
            }
        }
    }

    public void shapeLMovePlate()
    {
        Game sc = controller.GetComponent<Game>();
        int2[] posible =
        {
            new int2(1, 2),
            new int2(2, 1),
            new int2(-1, 2),
            new int2(-2, 1),
            new int2(-1, -2),
            new int2(-2, -1),
            new int2(1, -2),
            new int2(2, -1),
        };
        for (int i = 0; i < posible.Length; ++i)
        {
            int x = xBoard + posible[i].x;
            int y = yBoard + posible[i].y;
            pointMovePlate(x, y);
        }
    }

    public void surroundMovePlate()
    {
        pointMovePlate(xBoard + 1, yBoard);
        pointMovePlate(xBoard, yBoard + 1);
        pointMovePlate(xBoard - 1, yBoard);
        pointMovePlate(xBoard, yBoard - 1);
        pointMovePlate(xBoard + 1, yBoard + 1);
        pointMovePlate(xBoard + 1, yBoard - 1);
        pointMovePlate(xBoard - 1, yBoard + 1);
        pointMovePlate(xBoard - 1, yBoard - 1);
    }

    public void pawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.positionOnBoard(x, y))
        {
            if (sc.getPosition(x, y) == null)
            {
                movePlateSpawn(x, y);
            }
            
            if (sc.positionOnBoard(x + 1, y) && sc.getPosition(x + 1, y) != null && sc.getPosition(x + 1, y).GetComponent<Chessman>().player != this.player)
            {
                movePlateAttackSpawn(x + 1, y);
            }
            if (sc.positionOnBoard(x - 1, y) && sc.getPosition(x - 1, y) != null && sc.getPosition(x - 1, y).GetComponent<Chessman>().player != this.player)
            {
                movePlateAttackSpawn(x - 1, y);
            }
        }
    }
}
