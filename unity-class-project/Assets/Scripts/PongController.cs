using UnityEngine;
using socket.io;

public class PongController : MonoBehaviour
{

    private Vector2 pos1;
    private bool isDown = false;
    private Socket socket;

    void OnGUI()
    {

    }

    // Start is called before the first frame update

        void Start()
        {
            var serverUrl = "http://localhost:4990";
            socket = Socket.Connect(serverUrl);

            socket.On(SystemEvents.connect, () => {
                Debug.Log("Hello, Socket.io~");
            });

            socket.On(SystemEvents.reconnect, (int reconnectAttempt) => {
                Debug.Log("Hello, Again! " + reconnectAttempt);
            });

            socket.On(SystemEvents.disconnect, () => {
                Debug.Log("Bye~");
            });
        }


 

    // Update is called once per frame
    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;

                Debug.Log(pos);
                return;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            pos1 = Input.mousePosition;
            isDown = true;

            //Debug.Log(Input.mousePosition);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            isDown = false;
        }
        if (isDown) {
            Debug.Log(Input.mousePosition.y - pos1.y); // Change in Y coordinate to be send over socket
            socket.Emit("move", "{ y: " + (Input.mousePosition.y - pos1.y) + "}");
        }
        
    }
}
