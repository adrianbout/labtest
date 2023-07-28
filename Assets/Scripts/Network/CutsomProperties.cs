using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsomProperties : MonoBehaviour
{
    [SerializeField]
    private Text text;
    public Button button;
    private ExitGames.Client.Photon.Hashtable playerCP = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        button.onClick.AddListener(SetRandomNumber);
    }
    private void SetRandomNumber()
    {
        System.Random r = new System.Random();
        int rnb = r.Next(0, 99);
        playerCP["rnb"] = rnb;
    }
}
