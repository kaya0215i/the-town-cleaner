using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using The_Town_Cleaner.Shared;
using The_Town_Cleaner.Shared.Models.Entities;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    private async void Start() {

        Debug.Log(UserModel.I.LoadSaveData());

        return;

        await UserModel.I.CreateUserModel();

        User user = await UserModel.I.RegistUserAsync();
        Debug.Log($"Id：{user.Id}\n" +
            $"Name：{user.Name}");

        //await UserModel.I.SendScoreAsync(1, 150);

        var rankings = await UserModel.I.GetScoreRankingAsync(1);
        
        string txt = "";
        
        foreach (var ranking in rankings) {
            txt += $"Name：{ranking.name.ToString()}, Score：{ranking.score.ToString()}\n";
        }

        Debug.Log(txt);
    }
}