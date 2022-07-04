using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;


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
    }
}
