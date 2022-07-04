using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;
using System.Net.Mail;

namespace GDesign2022NFT.ViewModel.UserVMs
{
    public partial class UserVM : BaseCRUDVM<User>
    {

        public UserVM()
        {
        }

        protected override void InitVM()
        {
        }

        public override void Validate()
        {
           var item =  DC.Set<User>().FirstOrDefault(x => (x.Email.ToLower() == Entity.Email.ToLower()) || (x.IdentyCode.ToLower() == Entity.IdentyCode.ToLower()));
           if (item.Email.ToLower().Equals(Entity.Email.ToLower()))
           {
                MSD.AddModelError("Entity.Email","Email 已經報名過");
           }
           if (item.IdentyCode.ToLower().Equals(Entity.IdentyCode.ToLower()))
           {
                MSD.AddModelError("Entity.IdentyCode", "該 身分證/居留證 已經報名過");
           }

            if (!IsValidEmail(Entity.Email))
            {
                MSD.AddModelError("Entity.Email", "Email 格式不正確");
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
    }
}
