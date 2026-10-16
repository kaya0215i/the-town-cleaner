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
    }
}