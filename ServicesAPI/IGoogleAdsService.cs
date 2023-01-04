

using Google.Ads.GoogleAds.Lib;

namespace GoogleAdsAPI.ServicesAPI
{
    public interface IGoogleAdsService
    {
        void GetCampaign(long customerId);
        void GetSummurizeCampaign(long customerId);
        void GetAdGroup(long customerId, long? campaignId);
    }
}
