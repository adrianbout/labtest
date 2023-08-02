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
    private int currentHatIndex = -1;
    private int currentIndex = 0;

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
        customizationData = data;
        UpdateTitleText();
        SelectAccessories(customizationData.hat);

        string jsonString = JsonUtility.ToJson(data);
        if (customPropreties.ContainsKey("PlayerCustomization"))
        {
            customPropreties["PlayerCustomization"] = jsonString;
        }
        else
        {
            customPropreties.Add("PlayerCustomization", jsonString);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(customPropreties);
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
            if (changedProps.ContainsKey("PlayerCustomization"))
            {
                string jsonString = (string)changedProps["PlayerCustomization"];
                PlayerCustomizationData data = JsonUtility.FromJson<PlayerCustomizationData>(jsonString);
                ApplyCustomizationData(data);
            }
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
        title.text = customizationData.title.ToString();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (PV.IsMine)
            {
                currentIndex = (currentIndex + 1) % hats.Count;
                customizationData.hat = currentIndex;
                customizationData.title = currentIndex.ToString();
                UpdatePlayerCustomization(customizationData);
            }
        }
    }
}
