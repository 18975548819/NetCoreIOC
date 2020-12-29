using Freed.IdentityServer4.AuthenticationCenter.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Freed.IdentityServer4.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenApiController : ControllerBase
    {
        private readonly ILogger<TokenApiController> _logger;

        public TokenApiController(ILogger<TokenApiController> logger)
        {
            _logger = logger;
        }

        //[HttpPost, HttpGet]
        //public string GetToken([FromBody] RequestTokenModel model)
        //{
        //    var httpClient = new HttpClient();
        //    var disco = httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        //    {
        //        Address = $"{ Request.Scheme }://{Request.Host}"
        //    }).Result;
        //    if (disco.IsError)
        //    {
        //        throw new Exception(disco.Error);
        //    }
        //    var tokenResponse = httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    {
        //        Address = disco.TokenEndpoint,
        //        ClientId = model.ClientId,
        //        ClientSecret = model.ClientSecret,
        //        Scope = model.Scope
        //    });
        //    var json = tokenResponse.Result.Json;
        //    return json.ToString();
        //}

        //[Route("GetToken")]
        //[HttpPost,HttpGet]
        //public string GetToken(RequestTokenModel model)
        //{
        //    var httpClient = new HttpClient();
        //    var disco = httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        //    {
        //        //Address = $"{ Request.Scheme }://{Request.Host}"
        //        //一定要关闭Https请求，不然会抛异常
        //        Address = $"{ Request.Scheme }://{Request.Host}",
        //        Policy =
        //        {
        //             RequireHttps=false  //关闭Https请求
        //        }
        //    }).Result;
        //    if (disco.IsError)
        //    {
        //        throw new Exception(disco.Error);
        //    }
        //    var tokenResponse = httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        //    {
        //        Address = disco.TokenEndpoint,
        //        ClientId = model.ClientId,
        //        ClientSecret = model.ClientSecret,
        //        Scope = model.Scope
        //    });

        //    var json = tokenResponse.Result.Json;
        //    return json.ToString();
        //}


        [Route("GetToken")]
        [HttpPost, HttpGet]
        public ActionResult GetToken(RequestTokenModel model)
        {
            var httpClient = new HttpClient();
            var disco = httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                //Address = $"{ Request.Scheme }://{Request.Host}"
                //一定要关闭Https请求，不然会抛异常
                Address = $"{ Request.Scheme }://{Request.Host}",
                Policy =
                {
                     RequireHttps=false  //关闭Https请求
                }
            }).Result;
            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }
            var tokenResponse = httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = model.ClientId,
                ClientSecret = model.ClientSecret,
                Scope = model.Scope
            });

            var json = tokenResponse.Result.Json;
            var token = JsonConvert.DeserializeObject<TokenModel>(json.ToString());
            return Ok(token);
        }
    }
}

/// <summary>
/// 请求model
/// </summary>
public class RequestTokenModel
{
    /// <summary>
    /// IdentityServer项目Config.cs的GetClients方法中配置的访问客户端Id
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// IdentityServer项目Config.cs的GetClients方法中配置的ClientSecrets
    /// </summary>
    public string ClientSecret { get; set; }

    /// <summary>
    /// IdentityServer项目Config.cs的GetClients方法中配置的scope
    /// </summary>
    public string Scope { get; set; }
}
