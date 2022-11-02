using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI Highscore;
    // Start is called before the first frame update
    void Start()
    {
        Highscore.text = $"High Score: {SaveSystem.LoadPlayerHighScore()}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
