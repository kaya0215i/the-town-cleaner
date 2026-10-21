using Cysharp.Threading.Tasks;
using Grpc.Core;
using MagicOnion;
using MagicOnion.Client;
using System;
using System.Threading.Tasks;
using The_Town_Cleaner.Shared.Interfaces.Services;
using The_Town_Cleaner.Shared.Models.Entities;
using UnityEngine;

public class UserModel : Singleton<UserModel> {
#if DEBUG
    string serverURL = "http://localhost:5244";
#else
    string serverURL = "";
#endif

    // 自分のId
    public int UserId { get; private set; }

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
    public async UniTask ConnectionTest() {
        try {
            bool result = await client.ConnectionTestAsync();
            Debug.Log(result);
        } catch (Exception e) {
            Debug.LogException(e);
            throw;
        }
    }

    /// <summary>
    /// ユーザー登録
    /// </summary>
    public async UniTask<int> RegistUserAsync(string name) {
        UserId = await client.RegistUserAsync(name);
        return UserId;
    }

    /// <summary>
    /// スコア送信
    /// </summary>
    public async UniTask SendScoreAsync(int stageId, int score) {
        await client.SendScoreAsync(UserId, stageId, score);
    }

    /// <summary>
    /// スコアランキング取得
    /// </summary>
    public async UniTask<Ranking[]> GetScoreRankingAsync(int stageId) {
        return await client.GetScoreRankingAsync(stageId);
    }
}
