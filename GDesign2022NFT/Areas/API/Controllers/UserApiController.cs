using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using GDesign2022NFT.ViewModel.API.UserVMs;
using GDesign2022NFT.Model;
using System.Text;
using System.Security.Cryptography;
using GIGABYTE.Utility.Utility;
using GIGABYTE.Utility;

namespace GDesign2022NFT.Controllers
{
    [Area("API")]
    [AuthorizeJwt]
    [ActionDescription("使用者報名API")]
    [ApiController]
    [Route("api/User")]
    public partial class UserApiController : BaseApiController
    {
        private readonly ISmtpMail _smtpMail;
        public UserApiController(ISmtpMail smtpMail)
        {
            _smtpMail = smtpMail;
        }
        [ActionDescription("Sys.Search")]
        [HttpPost("Search")]
		public IActionResult Search(UserApiSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = Wtm.CreateVM<UserApiListVM>(passInit: true);
                vm.Searcher = searcher;
                return Content(vm.GetJson());
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }

        [ActionDescription("Sys.Get")]
        [HttpGet("{id}")]
        public UserApiVM Get(string id)
        {
            var vm = Wtm.CreateVM<UserApiVM>(id);
            return vm;
        }

        [ActionDescription("Sys.Create")]
        [HttpPost("Add")]
        [Public]
        public IActionResult Add(UserApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                var md5 = MD5Convert.GetMd5String("GDesignNFTVote", vm.Entity.Email);
                vm.Entity.SetPropertyValue("Md5Code", md5);
                vm.Entity.SetPropertyValue("AvtivityStatus", AvtivityStatus.NotAvtivity);
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    var url = $"http://localhost:8226/LoadImage/Index/{vm.Entity.Md5Code}";
                    _smtpMail.SendMail(new GIGABYTE.Utility.Dto.SmtpMailInputDto()
                    {
                        ToMails = new List<string>() { vm.Entity.Email },
                        Subject = "奇想 NFT",
                        BodyContent = $"<img src='{url}'><br/>若無法觀看圖片請<a href='{url}' target='_blank'>點此</a>",                        
                    });
                    return Ok(vm.Entity);
                }
            }

        }

        [ActionDescription("Sys.Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(UserApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

		[HttpPost("BatchDelete")]
        [ActionDescription("Sys.Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = Wtm.CreateVM<UserApiBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Sys.Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(UserApiSearcher searcher)
        {
            var vm = Wtm.CreateVM<UserApiListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("Sys.CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = Wtm.CreateVM<UserApiListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("Sys.DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = Wtm.CreateVM<UserApiImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);
        }

        [ActionDescription("Sys.Import")]
        [HttpPost("Import")]
        public ActionResult Import(UserApiImportVM vm)
        {
            if (vm!=null && (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData()))
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm?.EntityList?.Count ?? 0);
            }
        }


    }
}
