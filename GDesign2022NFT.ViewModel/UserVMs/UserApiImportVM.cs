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
    public partial class UserApiTemplateVM : BaseTemplateVM
    {
        [Display(Name = "名字")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<User>(x => x.Name);
        [Display(Name = "連絡電話")]
        public ExcelPropety Phone_Excel = ExcelPropety.CreateProperty<User>(x => x.Phone);
        public ExcelPropety IsForeigner_Excel = ExcelPropety.CreateProperty<User>(x => x.IsForeigner);
        [Display(Name = "學校名稱")]
        public ExcelPropety SchoolName_Excel = ExcelPropety.CreateProperty<User>(x => x.SchoolName);
        [Display(Name = "系所名稱")]
        public ExcelPropety SchoolDepartment_Excel = ExcelPropety.CreateProperty<User>(x => x.SchoolDepartment);
        [Display(Name = "就讀年級")]
        public ExcelPropety SchoolGrade_Excel = ExcelPropety.CreateProperty<User>(x => x.SchoolGrade);

	    protected override void InitVM()
        {
        }

    }

    public class UserApiImportVM : BaseImportVM<UserApiTemplateVM, User>
    {

    }

}
