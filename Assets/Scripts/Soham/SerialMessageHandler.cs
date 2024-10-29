using UnityEngine;

public class SerialMessageHandler : MonoBehaviour
{
    public SerialController serialController;

    void Start()
    {
        if (serialController == null)
            Debug.LogError("SerialController not assigned.");
    }

    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Display the message in the Console
        Debug.Log("Received: " + message);
    }

    // Message received from SerialController for connection status
    void OnConnectionEvent(bool isConnected)
    {
        Debug.Log(isConnected ? "Connected to Arduino" : "Disconnected from Arduino");
    }
}
