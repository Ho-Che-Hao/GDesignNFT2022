using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.UserVMs
{
    public partial class UserBatchVM : BaseBatchVM<User, User_BatchEdit>
    {
        public UserBatchVM()
        {
            ListVM = new UserListVM();
            LinkedVM = new User_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class User_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
