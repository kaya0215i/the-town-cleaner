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

        //await UserModel.I.SendScoreAsync(1, 150);

        var rankings = await UserModel.I.GetScoreRankingAsync(1);
        
        string txt = "";
        
        foreach (var ranking in rankings) {
            txt += $"Name：{ranking.name.ToString()}, Score：{ranking.score.ToString()}\n";
        }

        Debug.Log(txt);
    }
}