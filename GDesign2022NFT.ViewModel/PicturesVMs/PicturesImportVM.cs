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
        [Display(Name = "圖片名稱")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Pictures>(x => x.Name);
        public ExcelPropety Md5Code_Excel = ExcelPropety.CreateProperty<Pictures>(x => x.Md5Code);

	    protected override void InitVM()
        {
        }

    }

    public class PicturesImportVM : BaseImportVM<PicturesTemplateVM, Pictures>
    {

    }

}
