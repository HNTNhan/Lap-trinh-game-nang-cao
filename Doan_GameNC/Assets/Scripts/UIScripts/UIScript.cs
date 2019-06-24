using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private Text scoreText;

    private Player playerData;

    private void Awake()
    {
        playerData = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = playerData.GetCurrentAmmo() + " / \u221E";

        scoreText.text = "SCORE " + playerData.GetScore();
    }
}
