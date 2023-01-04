using CsvHelper;
using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Config;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V12.Errors;
using Google.Ads.GoogleAds.V12.Resources;
using Google.Ads.GoogleAds.V12.Services;
using Google.Api.Gax;
using GoogleAdsAPI.Models;
using GoogleAdsAPI.Options;
using Newtonsoft.Json;
using System.Data;

namespace GoogleAdsAPI.ServicesAPI
{
    public class GoogleAdsService : IGoogleAdsService
    {
        private readonly IConfiguration _configuration;

        public GoogleAdsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static readonly JsonSerializerSettings _options
      = new() { NullValueHandling = NullValueHandling.Ignore };
        public void GetCampaign(long customerId)
        {
            var section = _configuration.GetSection("GoogleAds");
            GoogleAdsConfig config = new GoogleAdsConfig(section);
            GoogleAdsClient client = new GoogleAdsClient(config);
            var fileName = "Test.json";

            // Get the GoogleAdsService.
            GoogleAdsServiceClient googleAdsService = client.GetService(
                Services.V12.GoogleAdsService);

            string query = @"SELECT campaign.id,campaign.name FROM campaign";


            googleAdsService.SearchStream(customerId.ToString(), query,
                    delegate (SearchGoogleAdsStreamResponse resp)
                    {

                        var json = SimpleWrite(resp, fileName);
                    });
        }
      
        public static object SimpleWrite(object obj, string fileName)
        {

            var jsonString = JsonConvert.SerializeObject(obj, _options);

            File.WriteAllText(fileName, jsonString);

            return fileName;
        }

        public void GetSummurizeCampaign(long customerId)
        {
            var section = _configuration.GetSection("GoogleAds");
            GoogleAdsConfig config = new GoogleAdsConfig(section);
            GoogleAdsClient client = new GoogleAdsClient(config);
            GoogleAdsServiceClient googleAdsService = client.GetService(
               Services.V12.GoogleAdsService);
            var fileName = "Summurize.json";
            string query =
                @"SELECT
                      campaign.id,
                      campaign.name,
                      campaign.status,
                      campaign.advertising_channel_type,
                      campaign.advertising_channel_sub_type,
                      campaign.start_date,
                      campaign.end_date,   
                      campaign.serving_status,
                      campaign.network_settings.target_google_search,
                      campaign.network_settings.target_search_network,
                      campaign.network_settings.target_content_network,
                      campaign.network_settings.target_partner_search_network
                    FROM
                      campaign";
            googleAdsService.SearchStream(customerId.ToString(), query,
                   delegate (SearchGoogleAdsStreamResponse resp)
                   {

                       var json = SimpleWrite(resp, fileName);
                   });
            //SearchGoogleAdsResponse response = googleAdsService.Search(customerId.ToString(), query);

            //using (StreamWriter file = File.CreateText("file.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, response);
            //}
        }


        public void GetAccountInformation(long customerId)
        {
            var section = _configuration.GetSection("GoogleAds");
            GoogleAdsConfig config = new GoogleAdsConfig(section);
            GoogleAdsClient client = new GoogleAdsClient(config);
            var fileName = "Test.json";

            // Get the GoogleAdsService.
            GoogleAdsServiceClient googleAdsService = client.GetService(
                Services.V12.GoogleAdsService);

            string query = @"SELECT customer.id, customer.descriptive_name, " +
                "customer.currency_code, customer.time_zone, customer.tracking_url_template, " +
                "customer.auto_tagging_enabled FROM customer LIMIT 1";

            SearchGoogleAdsRequest request = new SearchGoogleAdsRequest()
            {
                CustomerId = customerId.ToString(),
                Query = query
            };

            //CustomerModel customer = googleAdsService.Search(request).First().Customer;

            //var json = JsonConvert.SerializeObject(customer);
            //return json;
        }

        public void GetAdGroup(long customerId, long? campaignId)
        {
            var section = _configuration.GetSection("GoogleAds");
            GoogleAdsConfig config = new GoogleAdsConfig(section);
            GoogleAdsClient client = new GoogleAdsClient(config);
            GoogleAdsServiceClient googleAdsService = client.GetService(
               Services.V12.GoogleAdsService);

            var fileName = "AdGroup.json";
            string searchQuery = "SELECT campaign.id, ad_group.id, ad_group.name FROM ad_group";
            if (campaignId != null)
            {
                searchQuery += $" WHERE campaign.id = {campaignId}";
            }
            GoogleAdsRow response = new GoogleAdsRow();
            PagedEnumerable<SearchGoogleAdsResponse, GoogleAdsRow> searchPagedResponse =
                    googleAdsService.Search(customerId.ToString(), searchQuery);
            foreach (GoogleAdsRow googleAdsRow in searchPagedResponse)
            {


            }
            var json = SimpleWrite(response, fileName);


        }
    }
}

