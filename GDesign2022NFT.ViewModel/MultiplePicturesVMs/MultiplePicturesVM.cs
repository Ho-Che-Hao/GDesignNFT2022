using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;
using System.Security.Cryptography;

namespace GDesign2022NFT.ViewModel.MultiplePicturesVMs
{
    public partial class MultiplePicturesVM : BaseCRUDVM<MultiplePictures>
    {

        public MultiplePicturesVM()
        {
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

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        public Dictionary<Guid, string> GetImageMd5(List<Guid> photoId)
        {
            var result = new Dictionary<Guid,string>();
            var pictures = DC.Set<FileAttachment>().Where(x => photoId.Contains(x.ID));
            foreach (var picture in pictures)
            {
                var itemPath = GetServerMappath(picture.Path);
                var photoByte = System.IO.File.ReadAllBytes(itemPath);
                using (var cryptoMD5 = MD5.Create())
                {

                    //取得雜湊值位元組陣列
                    var hash = cryptoMD5.ComputeHash(photoByte);

                    //取得 MD5
                    var md5 = BitConverter.ToString(hash)
                        .Replace("-", string.Empty)
                        .ToUpper();
                    if (!result.ContainsValue(md5))
                    {
                        result.Add(picture.ID, md5);
                    }
                }
            }
            return result;
        }

        //todo : 正確放置位置?
        private string GetServerMappath(string input)
        {
            //string webRootPath = "_hostingEnvironment.ContentRootPath";
            string webRootPath = "E:\\WebsiteProgram\\2022NFT\\GDesign2022NFT\\GDesign2022NFT\\";
            var inputArr = input.Replace("/", "\\").Split("\\");
            var result = webRootPath;
            foreach (var item in inputArr)
            {

                if (!string.IsNullOrEmpty(item))
                {
                    //確保不會是空白
                    var itemPath = item.Trim();
                    switch (itemPath)
                    {
                        case ".":
                            //不動作 代表當下層
                            break;
                        case "..":
                            var webRootPathArr = result.Split("\\");
                            var webRootPathArrLength = webRootPathArr.Length;
                            if (webRootPathArrLength > 1)
                            {
                                result = string.Join("//", webRootPathArr.Take(webRootPathArrLength - 1));
                            }
                            //代表上一層，須將環境當下路徑去除尾段
                            break;
                        default:
                            result = $"{result}\\{itemPath}";
                            break;
                    }
                }
            }
            return result;
        }
    }
}
