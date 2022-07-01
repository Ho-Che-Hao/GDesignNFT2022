using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.MultiplePicturesVMs
{
    public partial class MultiplePicturesBatchVM : BaseBatchVM<MultiplePictures, MultiplePictures_BatchEdit>
    {
        public MultiplePicturesBatchVM()
        {
            ListVM = new MultiplePicturesListVM();
            LinkedVM = new MultiplePictures_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class MultiplePictures_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
