using Newtonsoft.Json;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : Singleton<SaveManager> {
    private string path = "/user.json";

    private void OnDisable() {
        SaveUserData();
    }
    protected override void OnDestroy() {
        OnDisable();
    }

    /// <summary>
    /// ユーザーデータ保存
    /// </summary>
    public void SaveUserData() {
        int saveId = UserModel.I.UserId;
        string saveName = UserModel.I.UserName;

        if (saveId == -1 || saveName == string.Empty) {
            Debug.Log("ユーザーIdもしくはユーザー名がありません。");
            return;
        }

        var setting = new JsonSerializerSettings {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        SaveData saveData = new SaveData() {
            id = saveId,
            name = saveName,
        };

        string saveJson = JsonConvert.SerializeObject(saveData, setting);
        var writer = new StreamWriter(Application.persistentDataPath + path);
        writer.Write(saveJson);
        writer.Flush();
        writer.Close();
    }

    /// <summary>
    /// ユーザーデータ読込
    /// </summary>
    public SaveData LoadUserData() {
        // データがあるか
        if (!File.Exists(Application.persistentDataPath + path)) {
            Debug.Log("ユーザーデータがありません。");
            throw new Exception("ユーザーデータがありません。");
        }

        var reader = new StreamReader(Application.persistentDataPath + path);
        string json = reader.ReadToEnd();
        reader.Close();
        try {
            return JsonConvert.DeserializeObject<SaveData>(json);
        }
        catch (Exception e) {
            Debug.LogException(e);
            throw e;
        }
    }
}

[System.Serializable]
public class SaveData {
    public int id;
    public string name;
}