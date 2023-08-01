using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonReadWriteSystem : MonoBehaviour
{
    public InputField idInputFeild;
    public InputField nameInputFeild;
    public InputField infoInputFeild;
    public Button Submit;
    public Button Load;
    private void Start()
    {
        Submit.onClick.AddListener(SaveToJson);
        Load.onClick.AddListener(LoadFromJson);
    }
    public void SaveToJson()
    {
        WeaponData weaponData = new WeaponData();
        weaponData.Id = idInputFeild.text;
        weaponData.Name = nameInputFeild.text;
        weaponData.Info = infoInputFeild.text;

        string json = JsonUtility.ToJson(weaponData);
        File.WriteAllText(Application.dataPath + "/WeaponDataFile.json", json);
    }
    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/WeaponDataFile.json");
        WeaponData data = JsonUtility.FromJson<WeaponData>(json);
        idInputFeild.text = data.Id;
        nameInputFeild .text = data.Name;
        infoInputFeild .text = data.Info;
    }
}
