using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSpawner : MonoBehaviour {

    [SerializeField] Transform pinSpawnLocation;
    [SerializeField] GameObject pins;
    GameObject currentPins;

    public void SpawnPins() {
        currentPins = Instantiate(pins, pinSpawnLocation);
    }

    public void ClearOldPins() {
        Destroy(currentPins);
    }

}
