using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;


public class adManager : MonoBehaviour {
    public GameObject GameManager;
    BannerView banner_view;
    RewardBasedVideoAd rewardBasedVideo;
    RewardBasedVideoAd rewardBasedVideoTest;
    InterstitialAd interstitial;
    AdRequest banner_request;
    string appId = "ca-app-pub-1499788680785494~3953034836";
    string bannerId = "ca-app-pub-1499788680785494/3392295420";
    string bannerId_test = "ca-app-pub-3940256099942544/6300978111"; // test Ad
    string videoId = "ca-app-pub-1499788680785494/3651875830";
    string videoId_test = "ca-app-pub-3940256099942544/5224354917"; //test Ad

    public void Start(){
        MobileAds.Initialize(appId);

        banner_view = new BannerView(bannerId, AdSize.SmartBanner, AdPosition.Top);
        banner_view.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Create an empty ad request.
        banner_request = new AdRequest.Builder().Build();

        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        RequestRewardBasedVideo();
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
    }

    public void RequestBanner()
    {
        // Load the banner with the request.
        banner_view.LoadAd(banner_request);
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("Banner failed to load: " + args.Message);
        // Handle the ad failed to load event.
    }


    public void destroy_banner()
    {
        banner_view.Destroy();
    }

    void RequestRewardBasedVideo(){
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, videoId);
    }

    public void RewardVideo(){
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            Debug.Log("Video Shown");
        }else{
            Debug.Log("Not Loaded mm");
        }
    }
 
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //string type = args.Type;
        //double amount = args.Amount;
        GameManager.GetComponent<userData>().hints_left += 2;
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
        Invoke("show", 1f);
        Debug.Log("Closed Reward");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }


    public void show(){
        Debug.Log("In Show");
        GameManager.GetComponent<rewardManager>().hint_fun();
        GameManager.GetComponent<userData>().update_ui();
    }

}
