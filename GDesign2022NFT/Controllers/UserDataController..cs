using GDesign2022NFT.Model;
using GDesign2022NFT.ViewModel.PicturesVMs;
using GDesign2022NFT.ViewModel.UserDataVMs;
using GDesign2022NFT.ViewModel.UserPostDataVMs;
using GDesign2022NFT.ViewModel.UserVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Mvc;

namespace GDesign2022NFT.Controllers
{
    public partial class UserData : BaseController
    {
        [HttpPost]
        [Public]
        public ActionResult Add(UserPostDataVM input)
        {
            var output = new UserPostDataResultVM();
            var privatekey = "GDesignNFTVote";
            var md5 = "";
            using (var cryptoMD5 = MD5.Create())
            {

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(Encoding.UTF8.GetBytes(String.Format("{0}{1}", privatekey, input.Email)));

                //取得 MD5
                md5 = BitConverter.ToString(hash)
                    .Replace("-", string.Empty)
                    .ToUpper();
            }
            var data = new UserVM();
            data.Entity = new User()
            {
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                SchoolName = input.SchoolName,
                SchoolDepartment = input.SchoolDepartment,
                SchoolGrade = input.SchoolGrade,
                IdentyCode = input.IdentyCode,
                IsForeigner = input.IsForeigner ? ForeignerTypeEnum.Foreigner : ForeignerTypeEnum.Native,
                AvtivityStatus = Model.AvtivityStatus.NotAvtivity,
                CreateBy = input.Name,
                Md5Code = md5
            };
            var vm = Wtm.CreateVM<UserVM>(data);
            vm.DoAdd();
            if (!ModelState.IsValid)
            {
                output.Message = "資料格式不正確";
                output.InnerData = ModelState.GetErrorJson();
            }
            else
            {
                output.Succsee = true;
                output.InnerData = input;
            }
            return Json(output);
        }
    }
}
