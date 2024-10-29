using UnityEngine;

public class ArduinoDataReader : MonoBehaviour
{
    public SerialController serialController;
    
    void Start()
    {
        // Initialize SerialController (assumes Ardity setup)
        if (serialController == null)
        {
            serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        }
    }

    void Update()
    {
        // Read data from Arduino
        string message = serialController.ReadSerialMessage();
        
        if (message == null) return;  // No new message

        // Check for Ardity's default connection/disconnection messages
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        {
            Debug.Log("Arduino connected!");
        }
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        {
            Debug.Log("Arduino disconnected!");
        }
        else
        {
            // Split the message based on the comma delimiter
            string[] values = message.Split(',');
            if (values.Length == 2)
            {
                int infiniteEncoderPosition = int.Parse(values[0]);
                int potValue = int.Parse(values[1]);

                // Output to Unity's Console
                Debug.Log("Infinite Encoder Position: " + infiniteEncoderPosition);
                Debug.Log("Potentiometer Value: " + potValue);
            }
        }
    }
}
