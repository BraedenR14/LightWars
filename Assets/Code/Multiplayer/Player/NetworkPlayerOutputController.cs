﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerOutputController : NetworkBehaviour, IPlayerViewOutputController
{
    private List<NetworkPlayerView> _networkPlayerViews;

    public void Awake()
    {
        _networkPlayerViews = new List<NetworkPlayerView>();
    }

    public void AddNetworkPlayerView(NetworkPlayerView playerView)
    {
        if (!isServer)
            return;
        
        _networkPlayerViews.Add(playerView);
    }

    public void DisplayPlayerHit(PlayerIds playerId, int damage)
    {
        // Run damage anim
    }

    public void GameOverWithWinner(PlayerIds playerId)
    {
        // Load winner stuff
    }

    public void UpdatePlayerView(PlayerData playerData)
    {
        if (!isServer)
            return;
        
        if (GameIsLoaded())
        {
            _networkPlayerViews[(int)playerData.id].UpdateEnergy(playerData.energy);
            _networkPlayerViews[(int)playerData.id].UpdateHealth(playerData.health);
            _networkPlayerViews[(int)playerData.id].SetPlayerId((int)(int)playerData.id);
        }
    }

    private bool GameIsLoaded()
    {
        return _networkPlayerViews.Count == 2;
    }

}
