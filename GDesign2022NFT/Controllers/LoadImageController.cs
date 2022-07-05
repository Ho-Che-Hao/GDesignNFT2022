using GDesign2022NFT.Model;
using GDesign2022NFT.ViewModel.PicturesVMs;
using GDesign2022NFT.ViewModel.UserVMs;
using GIGABYTE.Utility.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace GDesign2022NFT.Controllers
{
    public partial class LoadImageController : BaseController
    {
        private IFileRoot _fileRoot { set;get;}
        public LoadImageController(IFileRoot fileRoot) {
            _fileRoot = fileRoot;
        }

        [HttpGet]
        [Public]
        public ActionResult Index(string id)
        {
            //取得使用者資料
            var vmUserData = Wtm.CreateVM<UserVM>();
            vmUserData.SetUserVMByMd5(id);
            return SendPictureView(vmUserData);
        }

        private ActionResult SendPictureView(UserVM vmUserData)
        {
            var bytearray = new byte[0];
            var fileType = "";

            //
            if (vmUserData.Entity.ID == 0)
            {
                return NotFound("錯誤頁面");
            }

            if (vmUserData.SendPicture == null)
            {
                //todo: 隨機派發圖片
                var relationPictureIds = DC.Set<RelationUserPictures>().Select(x => x.PicturesId).AsQueryable();
                var notUseImage = DC.Set<Pictures>().Where(x => x.IsValid && !relationPictureIds.Contains(x.ID)).Select(x=> new {
                        Path = x.Photo.Path,
                        FileExt = x.Photo.FileExt,
                        ID = x.ID
                    }).OrderBy(x=> Guid.NewGuid()).First();
                    DC.AddEntity<RelationUserPictures>(new RelationUserPictures()
                    {
                        PicturesId = notUseImage.ID,
                        UsersId = vmUserData.Entity.ID
                    });
                    //vm.Entity.Photo.Path = notUseImage.Path;
                    //vm.Entity.Photo.FileExt = notUseImage.FileExt;
                    vmUserData.Entity.AvtivityStatus = AvtivityStatus.Avtivity;
                    vmUserData.DoEdit();
                    //DC.UpdateEntity<User>(vmUser);
                    DC.SaveChanges();
                    vmUserData.DoReInit();
            }

            //var vm = Wtm.CreateVM<PicturesVM>();
            //var path = vm.GetServerMappath(vmUserData.SendPicture.PhotoPath);
            var path = _fileRoot.ServerPath(vmUserData.SendPicture.PhotoPath);
            if (System.IO.File.Exists(path))
            {
                bytearray = System.IO.File.ReadAllBytes(path);
                fileType = $"image/{vmUserData.SendPicture.PhotoExt.Replace(".", "").ToLower()}";
            }
            return File(bytearray, fileType);
        }
    }
}
