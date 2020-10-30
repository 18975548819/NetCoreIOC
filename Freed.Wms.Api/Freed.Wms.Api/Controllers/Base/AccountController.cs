using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEntities.QueryModel;
using DataModel.Authorize;
using Freed.Common.Data;
using Freed.Wms.Api.Models;
using IBusinessManage;
using IDataService.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Freed.Wms.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IWmsPdaConfigManager _manager;
        private IJwtFactory _jwtFactory;

        public AccountController(IWmsPdaConfigManager manager, IJwtFactory jwtFactory) :base(manager)
        {
            _manager = manager;
            _jwtFactory = jwtFactory;
        }

        /// <summary>
        /// 获取配置信息集合
        /// </summary>
        /// <returns></returns>
        [Authorize,HttpGet, Route("get_wms_pda_config")]
        public async Task<IActionResult> GetWmsPdaConfig()
        {
            var condition = new WmsPdaConfigQuery();
            var query = new QueryData<WmsPdaConfigQuery>();
            query.Criteria = condition;

            var result = await _manager.GetPdaConfigAllAsync(query);

            return Ok(result);
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = new ErrData<string>();

            var condition = new WmsPdaUserQuery();
            condition.UFnumber = model.LoginName;
            condition.UPassword = model.Password;
            condition.GroupType = model.GroupType;
            var query = new QueryData<WmsPdaUserQuery>();
            query.Criteria = condition;

            var res = await _manager.LoginAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.Msg, res.Code);
            }
            else
            {
                var user = new LoginUser();
                user.IsAdmin = model.IsAdmin;
                user.UserNo = model.LoginName;
                user.UserName = res.Data.UFname;
                user.GroupType = model.GroupType;
                user.WmsRepertory = model.WmsRepertory;

                var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
                var tokenJson = await _jwtFactory.GenerateEncodedToken(user.UserNo, claimsIdentity);
                var token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);
                result.SetInfo(token.auth_token, "成功", 200);
            }

            return Ok(result);
        }
    }
}
