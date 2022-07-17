using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    Vector3 lastFramePosition;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.transform.position = new Vector3(9, 10, Camera.main.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentFramePosition.z = 0;

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {

            float newXPosition = Camera.main.transform.position.x;
            float newYPosition = Camera.main.transform.position.y;
            bool needToJump = false;

            if (lastFramePosition.x >= WorldController.Instance.World.Width) {
                needToJump = true;
                newXPosition = Camera.main.transform.position.x % WorldController.Instance.World.Width;
            }

            if (lastFramePosition.x < 0) {
                needToJump = true;
                newXPosition = Camera.main.transform.position.x + WorldController.Instance.World.Width;
            }

            if (lastFramePosition.y >= WorldController.Instance.World.Height)
            {
                needToJump = true;
                newYPosition = Camera.main.transform.position.y % WorldController.Instance.World.Height;
            }

            if (lastFramePosition.y < 0)
            {
                needToJump = true;
                newYPosition = Camera.main.transform.position.y + WorldController.Instance.World.Height;
            }

            if (needToJump) {

                //Vector3 testPosition = new Vector3(newXPosition, newYPosition, Camera.main.transform.position.z);
                //Debug.Log("     JUMP    ");
                //Debug.Log(" WorldController.Instance.World.Height   : " + WorldController.Instance.World.Height);
                //Debug.Log(" Cammera position                        : " + Camera.main.transform.position);
                //Debug.Log(" Last Frame Position                     : " + lastFramePosition);
                //Debug.Log("     testPosition                        : " + testPosition);

                Camera.main.transform.position = new Vector3(newXPosition, newYPosition, Camera.main.transform.position.z);
            }


            Vector3 difference = lastFramePosition - currentFramePosition;
            Camera.main.transform.Translate(difference);

        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }
}
