using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;
using System.Security.Cryptography;

namespace GDesign2022NFT.ViewModel.PicturesVMs
{
    public partial class PicturesVM : BaseCRUDVM<Pictures>
    {
        public string Message { set; get; }
        public PicturesVM()
        {
            
        }

        //執行順序
        // 一般 :  new PicturesVM => InitVM
        //post :  new PicturesVM => InitVM => 錯誤則使用 ReInitVM (正確直接往下一步) => 正確 Validate (包含使用 SetDuplicatedCheck) =>

        public override void Validate()
        {
            //todo: 驗證更複雜的資料
            var itemMd5 = GetImageMd5(Entity.PhotoId);
            var pictureItems = DC.Set<Pictures>().FirstOrDefault(x => x.Md5Code == itemMd5 && x.IsValid && x.ID != Entity.ID);
            if (pictureItems != null)
            {
                MSD.AddModelError("Entity.PhotoId", "圖片已經上傳過");
            }
            if (itemMd5 == "")
            {
                MSD.AddModelError("Entity.PhotoId", "請上傳圖片");
            }
            base.Validate();
        }

        protected override void InitVM()
        {
            //todo:定義初始化給值內容 dc 要在這裡使用            
        }

        protected override void ReInitVM()
        {
            //todo:定義 post 當發生錯誤時 再次執行此，絕大多說不需要，不寫以 InitVM 取代
            base.ReInitVM();
        }
        /*public override DuplicatedInfo<Pictures> SetDuplicatedCheck()
        {
            //todo: 確認資料表中的唯一值不可以重複
            return base.SetDuplicatedCheck();
            //var rv = CreateFieldsInfo(SimpleField(x => x.Md5Code));
            //兩組參數不可以重複CreateFieldsInfo(SimpleField(x => x.Md5Code),SimpleField(x => x.Md5Code));
            //若需要兩種條件
            //rv.AddGroup(SimpleField(x => x.PhotoId));
            //return rv;
        }*/

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

        public string GetImageMd5(Guid photoId)
        {
            string result = "";
            var picture = DC.Set<FileAttachment>().FirstOrDefault(x => x.ID == photoId);
            if (picture != null)
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
                    result = md5;
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
