using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    int matrixX, matrixY;

    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().getPosition(matrixX, matrixY);

            Destroy(cp);

            if (cp.name == "white_king")
            {
                controller.GetComponent<Game>().Winner("Black");
            }
            else if (cp.name == "black_king")
            {
                controller.GetComponent<Game>().Winner("White");
            }
        }
        controller.GetComponent<Game>().setPositionEmpty(reference.GetComponent<Chessman>().getXBoard(), reference.GetComponent<Chessman>().getYBoard());
        reference.GetComponent<Chessman>().setXBoard(matrixX);
        reference.GetComponent<Chessman>().setYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Game>().setPosition(reference);

        controller.GetComponent<Game>().nextTurn();

        reference.GetComponent<Chessman>().destroyMovePlate();
    }

    public void setCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
        
    public void setReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject getReference()
    {
        return reference;
    }
}
