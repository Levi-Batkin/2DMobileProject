using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessGPS : MonoBehaviour
{
    public bool connected = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        //checks if the app has permissions needed to access location
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation)) 
        {
            Debug.Log("No FineLocation permission, requesting");
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        }

        // Wait until the editor and unity remote are connected before starting a location service
        Debug.Log("Waiting");
        yield return new WaitForSeconds(5);
 
        // Check if location services are enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location is not enabled on this device");
            yield break;
        }
 
        // Start service before querying location
        Input.location.Start(1f);
        Debug.Log("Service started");

        //unity remote 5 takes a while to update, without this it assumes im trying to check the status without enabling locationj
        yield return new WaitForSeconds(5);

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            connected = true;
        }

        // Pings the location every 5s
        Input.location.Stop();
        yield return new WaitForSeconds(5);
        Input.location.Start();
    }

    public float GetLongitudeToX()
    {
        float lon = Input.location.lastData.longitude;
        float x = lon * 2 * Mathf.PI * 6378137 / 2 / 180;
        return x/1000;
    }

    public float GetLatitudeToY()
    {
        float lat = Input.location.lastData.latitude;
        float y = Mathf.Log(Mathf.Tan((90 + lat) * Mathf.PI / 360)) / (Mathf.PI / 180);
        y = y * 2 * Mathf.PI * 6378137 / 2 / 180;
        return y/1000;
    }

    public float ConvertLongitudeToX(float lon)
    {        
        float x = lon * 2 * Mathf.PI * 6378137 / 2 / 180;
        return x/1000;
    }

    public float ConvertLatitudeToY(float lat)
    {        
        float y = Mathf.Log(Mathf.Tan((90 + lat) * Mathf.PI / 360)) / (Mathf.PI / 180);
        y = y * 2 * Mathf.PI * 6378137 / 2 / 180;
        return y/1000;
    }


    public float GetHeading()
    {
        float rot = -Input.compass.magneticHeading;
        return rot;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
