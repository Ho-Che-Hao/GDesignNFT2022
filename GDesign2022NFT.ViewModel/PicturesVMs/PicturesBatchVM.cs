using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.PicturesVMs
{
    public partial class PicturesBatchVM : BaseBatchVM<Pictures, Pictures_BatchEdit>
    {
        public PicturesBatchVM()
        {
            ListVM = new PicturesListVM();
            LinkedVM = new Pictures_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Pictures_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
