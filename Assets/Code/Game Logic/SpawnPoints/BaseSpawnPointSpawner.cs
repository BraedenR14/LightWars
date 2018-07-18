﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnPointSpawner {

    protected List<SpawnPoint> _spawnPoints;

    private ISpawnPointLoader _spawnPointLoader;

    public BaseSpawnPointSpawner()
    {
        _spawnPoints = new List<SpawnPoint>();
    }

    public List<SpawnPoint> GetSpawnPoints()
    {
        return _spawnPoints;
    }

    public void SetSpawnPointLoader(ISpawnPointLoader spawnPointLoader)
    {
        _spawnPointLoader = spawnPointLoader;
    }

    public virtual void LoadSpawnPoints()
    {
        while(_spawnPointLoader.HasAnotherSpawnPoint())
        {
            _spawnPointLoader.LoadNextSpawnPoint();

            SpawnPoint nextSpawnPoint = new SpawnPoint();

            nextSpawnPoint.SetOwningPlayer(_spawnPointLoader.GetOwner());
            nextSpawnPoint.SetSiblingSpawnPoint(_spawnPointLoader.GetSibilingId());
            nextSpawnPoint.SetPosition(_spawnPointLoader.GetPosition());

            _spawnPoints.Add(nextSpawnPoint);
        }
    }

}
