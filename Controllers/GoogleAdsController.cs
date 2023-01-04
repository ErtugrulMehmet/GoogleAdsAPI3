using Google.Ads.GoogleAds.Lib;
using GoogleAdsAPI.Options;
using GoogleAdsAPI.ServicesAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAdsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAdsController : ControllerBase
    {
        private readonly IGoogleAdsService _googleAdsService;
       

        public GoogleAdsController(IGoogleAdsService googleAdsService)
        {
            _googleAdsService = googleAdsService;           
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCampaign(long customerId)
        {
            
            _googleAdsService.GetCampaign(customerId);          

            return Ok();
        }
        [HttpPost("GetSummurize")]
        public IActionResult GetSummurizeCampaign(long customerId) 
        {

            var result = _googleAdsService.GetSummurizeCampaign(customerId);

            return Ok(result);
        }
        [HttpGet]
        public IActionResult ListAccessibleAccounts()
        {

           var result=  _googleAdsService.ListAccessibleAccounts();

            return Ok(result);
        }
    }
}
