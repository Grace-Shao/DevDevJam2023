using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public static GameLogic Instance;

    private int highestScore = 0;
    private int wavesSurvived = 0;
    private int satisfiedClients = 0;
    private int unsatisfiedClients = 0;
    private int tiredClients = 0;
    private int foodsShot = 0;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
