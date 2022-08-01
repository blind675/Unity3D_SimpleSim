using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject cursor;

    Vector3 startTouchPosition;

    void Start()
    {
        // Center Camera
        Camera.main.transform.position = new Vector3(WorldController.Instance.World.Width / 2, WorldController.Instance.World.Height / 2, Camera.main.transform.position.z);
    }

    void Update()
    {
        SelectTile();
        UpdateCameraMovement();
        Zoom();
    }

    private void SelectTile()
    {
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        {
            Vector3 currentTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cursorPosition = new Vector3(Mathf.FloorToInt(currentTouchPosition.x), Mathf.FloorToInt(currentTouchPosition.y), 0);
            cursor.transform.position = cursorPosition;

            UIController.Instance.SelectedTile = WorldController.Instance.GetTileAtWorldCoord(cursorPosition);
        }
    }

    private void UpdateCameraMovement()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            startTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector3 currentTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float newXPosition = Camera.main.transform.position.x;
            float newYPosition = Camera.main.transform.position.y;
            bool needToJump = false;

            if (currentTouchPosition.x >= WorldController.Instance.World.Width)
            {
                needToJump = true;
                newXPosition = Camera.main.transform.position.x % WorldController.Instance.World.Width;
            }

            if (currentTouchPosition.x < 0)
            {
                needToJump = true;
                newXPosition = Camera.main.transform.position.x + WorldController.Instance.World.Width;
            }

            if (currentTouchPosition.y >= WorldController.Instance.World.Height)
            {
                needToJump = true;
                newYPosition = Camera.main.transform.position.y % WorldController.Instance.World.Height;
            }

            if (currentTouchPosition.y < 0)
            {
                needToJump = true;
                newYPosition = Camera.main.transform.position.y + WorldController.Instance.World.Height;
            }

            if (needToJump)
            {
                Camera.main.transform.position = new Vector3(newXPosition, newYPosition, Camera.main.transform.position.z);
            }

            Vector3 direction = startTouchPosition - currentTouchPosition;
            Camera.main.transform.position += direction;

        }
    }

    private void Zoom()
    {
        float zoomValue = Camera.main.orthographicSize - Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        if (Input.touchCount == 2)
        {
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

            float prevMagnitude = (touchOnePrevPos - touchTwoPrevPos).magnitude;
            float currentMagnitude = (touchOne.position - touchTwo.position).magnitude;

            float diference = currentMagnitude - prevMagnitude;

            zoomValue = Camera.main.orthographicSize -  diference * 0.05f;
        }

        zoomValue = Mathf.Clamp(zoomValue, 5f, 15);

        Camera.main.orthographicSize = zoomValue;
    }
}
