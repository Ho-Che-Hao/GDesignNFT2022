using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.ViewModel.PicturesVMs;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using GDesign2022NFT.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GDesign2022NFT.Utility;
using GDesign2022NFT.ViewModel.MultiplePicturesVMs;

namespace GDesign2022NFT.Controllers
{
    
    [ActionDescription("圖片編輯")]
    public partial class PicturesController : BaseController
    {
        public readonly PictureTestVm aa;
        private readonly IHostingEnvironment _hostingEnvironment;
        public PicturesController(PictureTestVm _aa, IHostingEnvironment hostingEnvironment)
        {
            aa = _aa;
            _hostingEnvironment = hostingEnvironment;
        }
        #region Search
        [ActionDescription("Sys.Search")]
        public ActionResult Index()
        {

            //aa.Test();
            var vm = Wtm.CreateVM<PicturesListVM>();
            var userItem = Wtm.LoginUserInfo;
            return PartialView(vm);
        }

        [ActionDescription("Sys.Search")]
        [HttpPost]
        public string Search(PicturesSearcher searcher)
        {
            var vm = Wtm.CreateVM<PicturesListVM>(passInit: true);
            if (ModelState.IsValid)
            {
                vm.Searcher = searcher;
                var item = new Dictionary<string, object>();
                item.Add("IsValid", searcher.IsValid);
                var itemData = vm.GetJson(false, func :()=> item);
                
                
                //Regex.Replace(itemData, "}}$",$"\nIsValid:{searcher.IsValid}}}");
                return itemData;
            }
            else
            {
                return vm.GetError();
            }
        }

        #endregion

        #region Create
        [ActionDescription("Sys.Create")]
        public ActionResult Create()
        {
            //todo : 多組上傳
            var vm = Wtm.CreateVM<MultiplePicturesVM>();
            vm.AutoFilterSame = false;
            return PartialView("~/Views/MultiplePictures/Create.cshtml", vm);
        }

        [ActionDescription("Sys.Create")]
        public ActionResult CreateAutoFilterSame()
        {
            //todo : 多組上傳
            var vm = Wtm.CreateVM<MultiplePicturesVM>();
            vm.AutoFilterSame = true;
            return PartialView("~/Views/MultiplePictures/Create.cshtml", vm);
        }

        /*[HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(PicturesVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.Entity.Md5Code = vm.GetImageMd5(vm.Entity.PhotoId);
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }*/

        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult Create(MultiplePicturesVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/MultiplePictures/Create.cshtml", vm);
            }
            else
            {
                vm.DoAddFilterSameFile(_hostingEnvironment);
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView("~/Views/MultiplePictures/Create.cshtml", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid();
                }
            }
        }
        [HttpPost]
        [ActionDescription("Sys.Create")]
        public ActionResult CreateAutoFilterSame(MultiplePicturesVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/MultiplePictures/Create.cshtml", vm);
            }
            else
            {
                vm.DoAddFilterSameFile(_hostingEnvironment);
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView("~/Views/MultiplePictures/Create.cshtml", vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.UpdloadCount.ToString()]);
                }
            }
        }
        #endregion

        #region Edit
        [ActionDescription("Sys.Edit")]
        public ActionResult Edit(string id)
        {
            var vm = Wtm.CreateVM<PicturesVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Edit")]
        [HttpPost]
        [ValidateFormItemOnly]
        public ActionResult Edit(PicturesVM vm)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                vm.DoEditFilterSameFile(_hostingEnvironment);
                if (!ModelState.IsValid)
                {
                    vm.DoReInit();
                    return PartialView(vm);
                }
                else
                {
                    return FFResult().CloseDialog().RefreshGridRow(vm.Entity.ID);
                }
            }
        }
        #endregion

        #region Delete
        [ActionDescription("Sys.Delete")]
        public ActionResult Delete(string id)
        {
            var vm = Wtm.CreateVM<PicturesVM>(id);
            return PartialView(vm);
        }

        [ActionDescription("Sys.Delete")]
        [HttpPost]
        public ActionResult Delete(string id, IFormCollection nouse)
        {
            var vm = Wtm.CreateVM<PicturesVM>(id);
            vm.DoDelete();
            if (!ModelState.IsValid)
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid();
            }
        }
        #endregion

        #region Details
        [ActionDescription("Sys.Details")]
        public ActionResult Details(string id)
        {
            var vm = Wtm.CreateVM<PicturesVM>(id);
            return PartialView(vm);
        }
        #endregion

        #region BatchEdit
        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult BatchEdit(string[] IDs)
        {
            var vm = Wtm.CreateVM<PicturesBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchEdit")]
        public ActionResult DoBatchEdit(PicturesBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchEdit())
            {
                return PartialView("BatchEdit",vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchEditSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region BatchDelete
        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult BatchDelete(string[] IDs)
        {
            var vm = Wtm.CreateVM<PicturesBatchVM>(Ids: IDs);
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.BatchDelete")]
        public ActionResult DoBatchDelete(PicturesBatchVM vm, IFormCollection nouse)
        {
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return PartialView("BatchDelete",vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.BatchDeleteSuccess", vm.Ids.Length]);
            }
        }
        #endregion

        #region Import
		[ActionDescription("Sys.Import")]
        public ActionResult Import()
        {
            var vm = Wtm.CreateVM<PicturesImportVM>();
            return PartialView(vm);
        }

        [HttpPost]
        [ActionDescription("Sys.Import")]
        public ActionResult Import(PicturesImportVM vm, IFormCollection nouse)
        {
            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return PartialView(vm);
            }
            else
            {
                return FFResult().CloseDialog().RefreshGrid().Alert(Localizer["Sys.ImportSuccess", vm.EntityList.Count.ToString()]);
            }
        }
        #endregion

        [ActionDescription("Sys.Export")]
        [HttpPost]
        public IActionResult ExportExcel(PicturesListVM vm)
        {
            return vm.GetExportData();
        }

    }
}
