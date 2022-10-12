using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class debugUi : MonoBehaviour
{
    GameObject _gameManager;
    AccessGPS _location;
    GameObject _player;

    public TextMeshProUGUI lat;
    public TextMeshProUGUI lon;

    public TextMeshProUGUI status;


    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        _location = _gameManager.GetComponent<AccessGPS>();
        //_player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        lat.text = "Lat: " + Input.location.lastData.latitude.ToString();
        lon.text = "Long: " + Input.location.lastData.longitude.ToString();

        status.text = "Connected: " + _location.connected.ToString();
        
    }
}
