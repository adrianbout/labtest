using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Add this namespace for using ExitGames.Client.Photon
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GLTFast.Schema;

public class PlayerCustomization : MonoBehaviourPunCallbacks
{
    public GameObject hatsContainer;
    public List<GameObject> hats;
    private PhotonView PV;
    private int currentIndex = 0;
    public TMP_Text playerName;
    private static string[] randomNames = { "Alpha", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "X-Ray", "Yankee", "Zulu" };
    private string playerNameString;

    Hashtable customPropreties = new Hashtable();

    private void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            //playerNameString = GetRandomName();
            //playerName.text = playerNameString;
            //customPropreties.Add("playerName", playerNameString);
            //PhotonNetwork.LocalPlayer.SetCustomProperties(customPropreties);
        }
    }
    private void AddHats()
    {
        foreach (Transform tr in hatsContainer.GetComponentsInChildren<Transform>())
        {
            hats.Add(tr.gameObject);
            Debug.Log("A");
        }
    }
    public void SelectAccessories(int index)
    {
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].SetActive(i == index);
        }
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(targetPlayer == null || changedProps == null)
            return;

        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            // Update customization for remote players based on custom properties change
            if (changedProps.ContainsKey("hatIndex"))
            {
                int remoteIndex = (int)changedProps["hatIndex"];
                SelectAccessories(remoteIndex);
            }

            if (changedProps.ContainsKey("playerName"))
            {
                //playerName.text = (string)changedProps["playerName"];
            }
        }
    }


    private string GetRandomName()
    {
        return randomNames[Random.Range(0, randomNames.Length)];
    }

    private void Update()
    {
        // Only execute the hat selection code for the local player
        if (PV.IsMine)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                currentIndex = (currentIndex + 1) % hats.Count;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                currentIndex = (currentIndex - 1 + hats.Count) % hats.Count;
            }

            // Update the hat index in custom properties for remote players
            customPropreties.Remove("hatIndex");
            customPropreties.Add("hatIndex", currentIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(customPropreties);

            // Update hat visibility for the local player
            SelectAccessories(currentIndex);
        }
    }
}
