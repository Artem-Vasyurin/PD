using UnityEngine;

public class FishingGame : MonoBehaviour
{
    public Transform area; // Область, включая полоску
    public Transform bar; // Полоска, которую нужно перемещать
    public Transform rectangle; // Прямоугольник, который автоматически перемещается

    public float areaTop = 2f; // Верхняя граница области
    public float areaBottom = -2f; // Нижняя граница области
    public float barSpeed = 5f; // Скорость перемещения полоски по вертикали
    public float rectangleSpeed = 1f; // Скорость перемещения прямоугольника по вертикали

    private float rectangleTop; // Верхняя граница прямоугольника
    private float rectangleBottom; // Нижняя граница прямоугольника

    private int score = 0; // Счет игры
    private float holdTime = 0f; // Время удержания полоски
    private bool isInsideRectangle = false; // Флаг, указывающий, находится ли полоска внутри прямоугольника

    void Start()
    {
        float barHeight = bar.localScale.y;
        float rectangleHeight = rectangle.localScale.y;

        areaTop -= rectangleHeight / 2f;
        areaBottom += rectangleHeight / 2f;

        rectangleTop = rectangle.position.y + rectangleHeight / 2f;
        rectangleBottom = rectangle.position.y - rectangleHeight / 2f;
    }

    void Update()
    {
        MoveBar();
        MoveRectangle();
        CheckBarPosition();
        CheckRectanglePosition();
    }

    void MoveBar()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float newYPosition = bar.position.y + verticalInput * barSpeed * Time.deltaTime;

        newYPosition = Mathf.Clamp(newYPosition, areaBottom, areaTop);

        Vector3 newPosition = new Vector3(bar.position.x, newYPosition, bar.position.z);
        bar.position = newPosition;

        if (newYPosition >= rectangleBottom && newYPosition <= rectangleTop)
        {
            holdTime += Time.deltaTime;
        }
    }

    void MoveRectangle()
    {
        float newYPosition = rectangle.position.y + rectangleSpeed * Time.deltaTime;

        if (newYPosition >= rectangleTop)
        {
            rectangleSpeed *= -1f;
        }
        else if (newYPosition <= rectangleBottom)
        {
            rectangleSpeed *= -1f;
        }

        rectangle.position += new Vector3(0f, rectangleSpeed * Time.deltaTime, 0f);
    }

    void CheckBarPosition()
    {
        if (isInsideRectangle && (bar.position.y < rectangleBottom || bar.position.y > rectangleTop))
        {
            isInsideRectangle = false;
            holdTime = 0f;
        }
        else if (!isInsideRectangle && bar.position.y >= rectangleBottom && bar.position.y <= rectangleTop)
        {
            isInsideRectangle = true;
        }

        if (isInsideRectangle && holdTime >= 1f)
        {
            score++;
            holdTime = 0f;
            Debug.Log("Очко! Счет: " + score);
        }
    }

    void CheckRectanglePosition()
    {
        if (rectangle.position.y >= areaTop)
        {
            rectangle.position = new Vector3(rectangle.position.x, areaTop, rectangle.position.z);
        }
        else if (rectangle.position.y <= areaBottom)
        {
            rectangle.position = new Vector3(rectangle.position.x, areaBottom, rectangle.position.z);
        }
    }
}