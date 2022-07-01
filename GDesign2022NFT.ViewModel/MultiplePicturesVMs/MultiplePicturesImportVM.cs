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
    public partial class MultiplePicturesTemplateVM : BaseTemplateVM
    {
        public ExcelPropety Md5Code_Excel = ExcelPropety.CreateProperty<MultiplePictures>(x => x.Md5Code);

	    protected override void InitVM()
        {
        }

    }

    public class MultiplePicturesImportVM : BaseImportVM<MultiplePicturesTemplateVM, MultiplePictures>
    {

    }

}
