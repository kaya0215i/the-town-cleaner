using Cysharp.Threading.Tasks;
using MagicOnion;
using MagicOnion.Client;
using System;
using System.Threading.Tasks;
using The_Town_Cleaner.Shared.Interfaces.Services;
using UnityEngine;

public class UserModel : Singleton<UserModel> {

#if DEBUG
    string serverURL = "http://localhost:5244";
#else
    string serverURL = "";
#endif
    private GrpcChannelx channelx;
    IUserService client;

    /// <summary>
    /// MagicOnion接続処理
    /// </summary>
    public UniTask CreateUserModel() {
        channelx = GrpcChannelx.ForAddress(serverURL);
        client = MagicOnionClient.Create<IUserService>(channelx);
        return UniTask.CompletedTask;
    }

    /// <summary>
    /// 接続テスト
    /// </summary>
    /// <returns></returns>
    public async UniTask ConnectionTest() {
        try {
            string result = await client.ConnectionTestAsync();
            Debug.Log(result);
        } catch (Exception e) {
            Debug.LogException(e);
            throw;
        }
    }
}
