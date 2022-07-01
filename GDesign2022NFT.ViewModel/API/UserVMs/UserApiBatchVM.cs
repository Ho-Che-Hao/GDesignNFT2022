using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.API.UserVMs
{
    public partial class UserApiBatchVM : BaseBatchVM<User, UserApi_BatchEdit>
    {
        public UserApiBatchVM()
        {
            ListVM = new UserApiListVM();
            LinkedVM = new UserApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class UserApi_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
