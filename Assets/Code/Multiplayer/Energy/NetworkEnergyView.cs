﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkEnergyView : NetworkBehaviour, IEnergySpawnable {

    [SyncVar(hook = "UpdateOwnerId")]
    private int _ownerId = -1;
    private void UpdateOwnerId(int ownerId)
    {
        _ownerId = ownerId;
    }

    public int GetOwnerId()
    {
        return _ownerId;
    }

    [SyncVar(hook = "UpdateSpeed")]
    private float _speed = -1;
    public void UpdateSpeed(float speed)
    {
        _speed = speed;
    }

    private float directionInverter = 1.0f;

    public void InitializeEnergy(int ownerId, float speed)
    {
        _ownerId = ownerId;
        _speed = speed;
    }

    public void Start()
    {
        if (Constants.LOCAL_PLAYER_ID == _ownerId)
        {
            directionInverter = 1.0f;
            transform.position = new Vector3(transform.position.x, Constants.LOCAL_PLAYER_POSITION.y, 0.0f);
        }
        else
        {
            directionInverter = -1.0f;
            transform.position = new Vector3(transform.position.x, Constants.NON_LOCAL_PLAYER_POSITION.y, 0.0f);
        }

        // Set color based on speed
        if (_speed < 4.0f && _speed > 3.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (_speed < 3.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }


    public void Update()
    {
        transform.position += new Vector3(0, (_speed * Time.deltaTime * directionInverter));
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Players")
        {
            if (collision.GetComponent<NetworkPlayerInput>().GetPlayerId() != _ownerId)
            {
                collision.GetComponent<NetworkPlayerInput>().PlayerHit(0);


                NetworkServer.Destroy(gameObject);
            }
        }
        else
        {
            if(collision.gameObject.GetComponent<NetworkEnergyView>().GetOwnerId() != _ownerId)
                NetworkServer.Destroy(gameObject);
        }
    }
}
