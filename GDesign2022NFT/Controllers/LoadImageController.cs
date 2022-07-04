using GDesign2022NFT.Model;
using GDesign2022NFT.ViewModel.PicturesVMs;
using GDesign2022NFT.ViewModel.UserVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace GDesign2022NFT.Controllers
{
    public partial class LoadImageController : BaseController
    {
        [HttpGet]
        [Public]
        public ActionResult Index(string id)
        {
            var bytearray = new byte[0];
            var fileType = "";

            //var vmUser = Wtm.CreateVM<UserVM>(x=>x.Entity.Md5Code == id);
            var vmUser = DC.Set<User>().FirstOrDefault(x=> x.Md5Code == id);

            if (vmUser != null)
            {
                var vm = Wtm.CreateVM<PicturesVM>();
                vm.Entity.Photo = new FileAttachment();
                var relationPicture = DC.Set<RelationUserPictures>().AsQueryable();
                var Pictures = DC.Set<Pictures>().Where(x=>x.IsValid).AsQueryable();
                var item = (from rel in relationPicture
                            join img in Pictures
                            on rel.PicturesId equals img.ID
                            where rel.UsersId == vmUser.ID
                            select new { Path = img.Photo.Path, PhotoId = img.ID , PhotoExt = img.Photo.FileExt }).FirstOrDefault();
                if (item == null)
                {
                    //todo: 派發圖片
                    var notUseImage = Pictures.Where(x => !relationPicture.Select(z => z.PicturesId).Contains(x.ID)).Select(x=> new {
                        Path = x.Photo.Path,
                        FileExt = x.Photo.FileExt,
                        ID = x.ID
                    }).OrderBy(x=> Guid.NewGuid()).First();
                    DC.AddEntity<RelationUserPictures>(new RelationUserPictures()
                    {
                        PicturesId = notUseImage.ID,
                        UsersId = vmUser.ID
                    });
                    vm.Entity.Photo.Path = notUseImage.Path;
                    vm.Entity.Photo.FileExt = notUseImage.FileExt;
                    vmUser.AvtivityStatus = AvtivityStatus.Avtivity;
                    DC.UpdateEntity<User>(vmUser);
                   DC.SaveChanges();
                }
                else
                {
                    vm.Entity.Photo.Path = item.Path;
                    vm.Entity.Photo.FileExt = item.PhotoExt;
                }

                //todo: 顯示圖片
                
                var path = vm.GetServerMappath(vm.Entity.Photo.Path);
                if (System.IO.File.Exists(path))
                {
                    bytearray = System.IO.File.ReadAllBytes(path);
                    fileType = $"image/{vm.Entity.Photo.FileExt.Replace(".", "").ToLower()}";
                }
                //var vmPicture = Wtm.CreateVM<PicturesVM>(context => context.Entity.Md5Code.Equals(id));
            }
            /*var vm = Wtm.CreateVM<PicturesVM>(id);
            var bytearray = new byte[0];
            var fileType = "";
            if (vm != null && vm.Entity.Photo != null)
            {
                var path = vm.GetServerMappath(vm.Entity.Photo.Path);
                if (System.IO.File.Exists(path))
                {
                    bytearray = System.IO.File.ReadAllBytes(path);
                    fileType = $"image/{vm.Entity.Photo.FileExt.Replace(".", "").ToLower()}";
                }
            }*/
            return File(bytearray, fileType);
        }
    }
}
