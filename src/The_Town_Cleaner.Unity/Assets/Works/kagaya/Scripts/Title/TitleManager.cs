using System;
using The_Town_Cleaner.Shared.Models.Entities;
using UnityEngine;

public class TitleManager : MonoBehaviour {
    private void Awake() {
        // フレームレートを60に固定
        Application.targetFrameRate = 60;

        // Randomの設定
        DateTime dt = DateTime.Now;
        int iDate = int.Parse(dt.ToString("MMddHHmmss"));
        UnityEngine.Random.InitState(iDate);
    }

    private async void Start() {
        // 接続
        await UserModel.I.CreateUserModel();

        // セーブデータ読込
        bool result = await UserModel.I.LoadSaveData();
        // 無かったら
        if (!result) {
            // ユーザー登録
            User user = await UserModel.I.RegistUserAsync();
            Debug.Log($"Id：{user.Id}\n" +
                $"Name：{user.Name}");
        }
    }
}
