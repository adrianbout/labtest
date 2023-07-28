using NaughtyAttributes;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerName : MonoBehaviourPunCallbacks
{
    public PhotonView pv;
    public TMP_Text playerName;
    private static string[] randomNames = { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-Ray", "Yankee", "Zulu" };

    private void Start()
    {
        if (pv == null)
            pv = GetComponent<PhotonView>();


        if (PhotonNetwork.IsConnected)
        {
            string randomName = GetRandomName();
            photonView.RPC("SetPlayerNames", RpcTarget.All, randomName);
        }
        else
        {
            Debug.LogWarning("Not connected to Photon. Make sure you have initialized Photon correctly.");
        }
    }
    private string GetRandomName()
    {
        return randomNames[Random.Range(0, randomNames.Length)] + Random.Range(1000, 9999);
    }

    [PunRPC]
    public void SetPlayerNames(string name)
    {
        playerName.text = name;
    }
}
