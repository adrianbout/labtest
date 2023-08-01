using UnityEngine;
using Photon.Pun;
using TMPro;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayerCustomizationController : MonoBehaviourPunCallbacks
{
    private PlayerCustomizationData customizationData;
    private Hashtable customPropreties;
    private PhotonView PV;
    public TMP_Text title;
    public List<GameObject> hats;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        customPropreties = new Hashtable();
        customizationData = new PlayerCustomizationData { hat = 1, title = "" };
        UpdatePlayerCustomization(customizationData);
    }
    public void SelectAccessories(int index)
    {
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].SetActive(i == index);
        }
    }

    public void UpdatePlayerCustomization(PlayerCustomizationData data)
    {
        string jsonString = JsonUtility.ToJson(data);
        customPropreties.Remove("PlayerCustomization");
        customPropreties.Add("PlayerCustomization", jsonString);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customPropreties);
        UpdateTitleText();
        SelectAccessories(customizationData.hat);
    }

    public PlayerCustomizationData GetPlayerCustomization()
    {
        string jsonString = (string)PhotonNetwork.LocalPlayer.CustomProperties["PlayerCustomization"];
        return JsonUtility.FromJson<PlayerCustomizationData>(jsonString);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer == null || changedProps == null)
            return;

        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            string jsonString = (string)targetPlayer.CustomProperties["PlayerCustomization"];
            PlayerCustomizationData data = JsonUtility.FromJson<PlayerCustomizationData>(jsonString);
            ApplyCustomizationData(data);
        }
    }

    private void ApplyCustomizationData(PlayerCustomizationData data)
    {
        customizationData = data;
        UpdateTitleText();
        SelectAccessories(data.hat);
    }

    private void UpdateTitleText()
    {
        string newTitle = customizationData.title.ToString();
        PV.RPC("UpdateTitleRPC", RpcTarget.AllBuffered, newTitle);
    }

    [PunRPC]
    public void UpdateTitleRPC(string newTitle)
    {
        title.text = newTitle;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (PV.IsMine)
            {
                customizationData.hat += 1;
                customizationData.title = "agfr";
                UpdatePlayerCustomization(customizationData);
            }
        }
    }
}
