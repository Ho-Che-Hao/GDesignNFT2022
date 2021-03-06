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
    public partial class PicturesTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class PicturesImportVM : BaseImportVM<PicturesTemplateVM, Pictures>
    {

    }

}
