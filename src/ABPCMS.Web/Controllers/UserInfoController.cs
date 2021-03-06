﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Web.Models;
using Abp.Web.Security.AntiForgery;
using ABPCMS.Authorization;
using ABPCMS.Authorization.Roles;
using ABPCMS.Users;
using ABPCMS.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ABPCMS.Web.Controllers
{
    public class UserInfoController : ABPCMSControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly RoleManager _roleManager;

        public UserInfoController(IUserAppService userAppService, RoleManager roleManager)
        {
            _userAppService = userAppService;
            _roleManager = roleManager;
        }
        public ActionResult Index()
        {
            return View();
        }
        [AbpAuthorize(PermissionNames.Pages_UserInfos_Update)]
        [HttpGet]
        [DontWrapResult]

        public async Task<ActionResult> GetUserInfo()
        {
            string pageNumber = string.IsNullOrWhiteSpace(Request["pageNumber"]) ? "0" : Request["pageNumber"];
            string pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? "20" : Request["pageSize"];
            var users = (await _userAppService.GetAll(new PagedResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            var  Userlist = users.Skip(int.Parse(pageNumber) * int.Parse(pageSize)).Take(int.Parse(pageSize)).ToList();
            int totaldata = Userlist.Count();
            var result = new { total = 10, rows = Userlist };
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}