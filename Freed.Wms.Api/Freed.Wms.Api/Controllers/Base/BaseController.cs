using DataEntities.InterfaceEntities;
using DataEntities.InterfaceModel;
using DataEntities.QueryModel;
using DataModel.Authorize;
using IBusinessManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Controllers.Base
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseController : ControllerBase
    {
        private readonly IWmsPdaConfigManager _configManager;
        public BaseController(IWmsPdaConfigManager configManager)
        {
            _configManager = configManager;
        }

        /// <summary>
        /// 获取远程客户端的IP地址
        /// </summary>
        /// <returns></returns>
        protected System.Net.IPAddress GetClientIp()
        {
            return HttpContext.Connection.RemoteIpAddress;
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public LoginUser CurrentUser
        {
            get
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var user = new LoginUser();
                foreach (var claim in claimIdentity.Claims)
                {
                    switch (claim.Type)
                    {
                        case "userNo":
                            user.UserNo = claim.Value;
                            break;
                        case ClaimTypes.Name:
                            user.UserName = claim.Value;
                            break;
                        case "isAdmin":
                            user.IsAdmin = Convert.ToBoolean(claim.Value);
                            break;
                        case "groupType":
                            user.GroupType = claim.Value;
                            break;
                        case "wmsRepertory":
                            user.WmsRepertory = claim.Value;
                            break;
                        default:
                            break;
                    }
                }
                return user;
            }
        }

        /// <summary>
        /// 根据token拼接登录厂区的数据库连接串
        /// </summary>
        public string CurrentConnFactory
        {
            get
            {
                if (!string.IsNullOrEmpty(CurrentUser.GroupType))
                {
                    var query = new WmsPdaConfigQuery();
                    query.GroupType = CurrentUser.GroupType;

                    var result = _configManager.QueryWmsPdaConnAsync(query);
                    if (!result.Result.HasErr)
                    {
                        return result.Result.Data;
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据Token获取FeasId
        /// </summary>
        public string CurrentFeasId
        {
            get
            {
                if (!string.IsNullOrEmpty(CurrentUser.GroupType))
                {
                    var configQuery = new WmsPdaConfigQuery();
                    configQuery.GroupType = CurrentUser.GroupType;
                    var result = _configManager.QueryWmsPdaConfigAsync(configQuery);
                    if (!result.Result.HasErr)
                    {
                        return result.Result.Data.FeasId;
                    }
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据Token获取当前用户的配置信息
        /// </summary>
        public IWmsPdaConfig CurrentConfig
        {
            get
            {
                if (!string.IsNullOrEmpty(CurrentUser.GroupType))
                {
                    var configQuery = new WmsPdaConfigQuery();
                    configQuery.GroupType = CurrentUser.GroupType;
                    var result = _configManager.QueryWmsPdaConfigAsync(configQuery);
                    if (!result.Result.HasErr)
                    {
                        return result.Result.Data;
                    }
                }
                return new WmsPdaConfigModel();
            }
        }

        /// <summary>
        /// 根据Token获取UnitFid
        /// </summary>
        public string CurrentUnitFid
        {
            get
            {
                if (!string.IsNullOrEmpty(CurrentUser.GroupType))
                {
                    var configQuery = new WmsPdaConfigQuery();
                    configQuery.GroupType = CurrentUser.GroupType;
                    var result = _configManager.QueryWmsPdaConfigAsync(configQuery);
                    if (!result.Result.HasErr)
                    {
                        return result.Result.Data.UnitFid;
                    }
                }
                return string.Empty;
            }
        }


        /// <summary>
        /// 检测图形验证码是否匹配
        /// </summary>
        /// <param name="valcode"></param>
        protected void MatchAuthCode(string valcode)
        {

            if (valcode == null)
            {
                valcode = "";
            }
            var nowval = HttpContext.Session.GetString("valcode");
            if (nowval == null)
            {
                nowval = "";
            }
            if (nowval.ToUpper() != valcode.ToUpper())
            {
                throw new Exception("验证码错误。");
            }
        }
    }
}
