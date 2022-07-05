using System;
using System.Linq;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using GIGABYTE.Utility.Utility;
//using Microsoft.AspNetCore.Hosting.Internal;



namespace GDesign2022NFT.ViewModel.PicturesVMs
{
    public partial class PicturesVM : BaseCRUDVM<Pictures>
    {
        public string Message { set; get; }


        //執行順序
        // 一般 :  new PicturesVM => InitVM
        //post :  new PicturesVM => InitVM => 錯誤則使用 ReInitVM (正確直接往下一步) => 正確 Validate (包含使用 SetDuplicatedCheck) =>

        public override void Validate()
        {
            //todo: 驗證更複雜的資料
            if (Entity.Photo == null)
            {
                //MSD.AddModelError("Entity.PhotoId", "請上傳圖片");
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

        public void DoEditFilterSameFile(IHostingEnvironment hostingEnvironment,bool updateAllFields = false)
        {
            var picture = DC.Set<FileAttachment>().FirstOrDefault(x => x.ID == Entity.PhotoId);
            if (picture != null)
            {
                var itemPath = Path.Combine(hostingEnvironment.ContentRootPath, picture.Path);
                var photoByte = System.IO.File.ReadAllBytes(itemPath);
                var md5 = MD5Convert.GetMd5String(photoByte);
                var pictureItems = DC.Set<Pictures>().FirstOrDefault(x => x.Md5Code == md5 && x.IsValid && x.ID != Entity.ID);
                if (pictureItems != null)
                {
                    MSD.AddModelError("Entity.PhotoId", "圖片已經上傳過");
                }
                else
                {
                    Entity.SetPropertyValue("Md5Code", md5);
                    base.DoEdit(updateAllFields);
                }
            }
           
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        
    }
}
