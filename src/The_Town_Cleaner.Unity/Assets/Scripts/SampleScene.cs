using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using The_Town_Cleaner.Shared;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    private async void Start() {
        await UserModel.I.CreateUserModel();
        UserModel.I.ConnectionTest().Forget();

        //int userId = await UserModel.I.RegistUserAsync("Player_10");
        //Debug.Log(userId);

        //await UserModel.I.SendScoreAsync(2, 500);

        var result = await UserModel.I.GetScoreRankingAsync(1);
        
        string txt = "";
        
        foreach (var item in result) {
            txt += $"Name：{item.Item1.ToString()}, Score：{item.Item2.ToString()}\n";
        }

        Debug.Log(txt);
    }
}