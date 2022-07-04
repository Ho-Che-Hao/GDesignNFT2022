using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;
using System.Net.Mail;

namespace GDesign2022NFT.ViewModel.API.UserVMs
{
    public partial class UserApiVM : BaseCRUDVM<User>
    {

        public UserApiVM()
        {
        }

        protected override void InitVM()
        {
        }
        public override void Validate()
        {
            var item = DC.Set<User>().FirstOrDefault(x => (x.Email == Entity.Email) || (x.IdentyCode == Entity.IdentyCode));
            if (item != null)
            {
                if (item.Email.Equals(Entity.Email))
                {
                    MSD.AddModelError("Entity.Email", "Email 已經報名過");
                }
                if (item.IdentyCode.Equals(Entity.IdentyCode))
                {
                    MSD.AddModelError("Entity.IdentyCode", "該 身分證/居留證 已經報名過");
                }
            }
            if (!IsValidEmail(Entity.Email))
            {
                MSD.AddModelError("Entity.Email", "Email 格式不正確");
            }
            if (Entity.IsForeigner == ForeignerTypeEnum.Native && !isIdentificationId(Entity.IdentyCode))
            {
                MSD.AddModelError("Entity.IdentyCode", "身分證格式不正確");
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

        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool isIdentificationId(string arg_Identify) { var d = false; if (arg_Identify.Length == 10) { arg_Identify = arg_Identify.ToUpper(); if (arg_Identify[0] >= 0x41 && arg_Identify[0] <= 0x5A) { var a = new[] { 10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33 }; var b = new int[11]; b[1] = a[(arg_Identify[0]) - 65] % 10; var c = b[0] = a[(arg_Identify[0]) - 65] / 10; for (var i = 1; i <= 9; i++) { b[i + 1] = arg_Identify[i] - 48; c += b[i] * (10 - i); } if (((c % 10) + b[10]) % 10 == 0) { d = true; } } } return d; }
    }
}
