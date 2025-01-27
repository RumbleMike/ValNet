﻿using System.Text.Json;
using RestSharp;
using ValNet.Objects.Store;

namespace ValNet.Requests;

public class Store : RequestBase
{
    public Store(RiotUser pUser) : base(pUser)
    {
        _user = pUser;
    }

    public PlayerStore PlayerStore { get; set; }

    public async Task<PlayerStore> GetPlayerStore()
    {
        var resp = await RiotPdRequest($"/store/v2/storefront/{_user.UserData.sub}", Method.Get);

        if (!resp.isSucc)
            throw new Exception("Failed to get Player Store");

        PlayerStore = JsonSerializer.Deserialize<PlayerStore>(resp.content.ToString());

        return PlayerStore;
    }

    public async Task<object> GetStoreOffers()
    {
        var resp = await RiotPdRequest("/store/v1/offers/", Method.Get);

        if (!resp.isSucc)
            throw new Exception("Failed to get Store Offers");

        return resp.content;
    }

    public async Task<PlayerWallet> GetPlayerBalance()
    {
        var resp = await RiotPdRequest($"/store/v1/wallet/{_user.UserData.sub}", Method.Get);
        
        if (!resp.isSucc)
            throw new Exception("Failed to get Player Balance");

        
        return JsonSerializer.Deserialize<PlayerWallet>(resp.content.ToString());
    }
}