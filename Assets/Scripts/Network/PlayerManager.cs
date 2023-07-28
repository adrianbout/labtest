using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Realtime;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using System;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView pv;
    public InputField inputField;
    private Button createPlayerButton;
    public TMP_Dropdown dropDownPlayer;
    private int playerCharacterIndex;
    private void Start()
    {
        pv = GetComponent<PhotonView>();
        //inputField = GameObject.FindWithTag("RPMInput").GetComponent<InputField>();
        dropDownPlayer = GameObject.FindWithTag("DropdownCharacterSelect").GetComponent<TMP_Dropdown>();
        createPlayerButton = GameObject.FindGameObjectWithTag("CreatePlayer").GetComponent<Button>();

        createPlayerButton.onClick.AddListener(() => CreateController(SelectedCharcter(playerCharacterIndex)));
        dropDownPlayer.onValueChanged.AddListener(UpdateMainButtonText);
    }
    private void UpdateMainButtonText(int value)
    {
        playerCharacterIndex = value;
    }

    private void CreateController(string playerURL)
    {
        if (pv.IsMine)
        {
            GameObject playerPrefab = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "RPM_Photon_Test_Character"), Vector3.zero, Quaternion.identity);
            playerPrefab.GetComponentInChildren<NetworkPlayer>().LoadAvatar(playerURL);
        }
    }
    private string SelectedCharcter(int value)
    {
        switch (value)
        {
            case 0:
                return "https://models.readyplayer.me/64ae92e2b638dd50f08948b7.glb";
            case 1:
                return "https://models.readyplayer.me/64b00706c0cd899011933f50.glb";
            default:
                return "https://models.readyplayer.me/64b00706c0cd899011933f50.glb";

        }
    }
}