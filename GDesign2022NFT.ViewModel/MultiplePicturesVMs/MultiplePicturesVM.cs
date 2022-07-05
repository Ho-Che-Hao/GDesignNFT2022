using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Model;
using GDesign2022NFT.ViewModel.PicturesVMs;
using GIGABYTE.Utility.Utility;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GDesign2022NFT.ViewModel.MultiplePicturesVMs
{
    public partial class MultiplePicturesVM : BaseCRUDVM<MultiplePictures>
    {
        public bool AutoFilterSame { set; get; }

        public int UpdloadCount { set; get; }

        public MultiplePicturesVM()
        {
            AutoFilterSame = false;
            UpdloadCount = 0;
        }
        protected override void InitVM()
        {

        }

        public override void Validate()
        {
            if (Entity.Photos == null)
            {
                MSD.AddModelError("Entity.Photos", "請至少上傳一張圖片");
            }
            base.Validate();
        }
        public override void DoAdd()
        {
            base.DoAdd();
        }

        public void DoAddFilterSameFile(IHostingEnvironment hostingEnvironment)
        {
            base.DoAdd();
            //圖片新增需要順便新增 md5
            var allowMD5Items = GetImageMd5(Entity.Photos.Select(x => x.FileId).ToList(), hostingEnvironment);
            var allowMD5FileIds = allowMD5Items.Select(x => x.Key).ToList();
            var items = DC.Set<MultiplePicturesUpload>().Where(x => allowMD5FileIds.Contains(x.FileId)).ToList();
            if(!AutoFilterSame && allowMD5FileIds.Count != Entity.Photos.Count)
            {
                MSD.AddModelError("Entity.PhotoId", "圖片中含有已經上傳過或重複圖片");
            }
            else
            {
                foreach (var MD5item in allowMD5Items)
                {
                    var item = items.FirstOrDefault(x => x.FileId.Equals(MD5item.Key));
                    if (item != null)
                    {
                        var picture = Wtm.CreateVM<PicturesVM>();
                        picture.Entity.Md5Code = MD5item.Value;
                        picture.Entity.PhotoId = MD5item.Key;
                        picture.DoAdd();
                        this.UpdloadCount++;
                    }

                }
            }
            
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        public Dictionary<Guid, string> GetImageMd5(List<Guid> photoId, IHostingEnvironment hostingEnvironment)
        {
            var result = new Dictionary<Guid,string>();
            var pictures = DC.Set<FileAttachment>().Where(x => photoId.Contains(x.ID));
            var ExistItems = DC.Set<Pictures>().Where(x => x.IsValid).Select(x=>x.Md5Code).ToList();
            foreach (var picture in pictures)
            {
                //var itemPath = GetServerMappath(picture.Path);
                //var itemPath = _fileRoot.ServerPath(picture.Path);
                var itemPath = Path.Combine(hostingEnvironment.ContentRootPath, picture.Path);
                var photoByte = System.IO.File.ReadAllBytes(itemPath);
                var md5 = MD5Convert.GetMd5String(photoByte);
                if (!result.ContainsValue(md5) && !ExistItems.Contains(md5) && !string.IsNullOrEmpty(md5))
                {
                    result.Add(picture.ID, md5);
                }
            }
            return result;
        }
    }
}
