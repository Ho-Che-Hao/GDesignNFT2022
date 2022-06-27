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
    public partial class UserTemplateVM : BaseTemplateVM
    {
        [Display(Name = "名字")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<User>(x => x.Name);
        [Display(Name = "信箱")]
        public ExcelPropety Email_Excel = ExcelPropety.CreateProperty<User>(x => x.Email);
        [Display(Name = "連絡電話")]
        public ExcelPropety Phone_Excel = ExcelPropety.CreateProperty<User>(x => x.Phone);
        public ExcelPropety IsForeigner_Excel = ExcelPropety.CreateProperty<User>(x => x.IsForeigner);
        [Display(Name = "學校名稱")]
        public ExcelPropety SchoolName_Excel = ExcelPropety.CreateProperty<User>(x => x.SchoolName);
        [Display(Name = "系所名稱")]
        public ExcelPropety SchoolDepartment_Excel = ExcelPropety.CreateProperty<User>(x => x.SchoolDepartment);
        [Display(Name = "就讀年級")]
        public ExcelPropety SchoolGrade_Excel = ExcelPropety.CreateProperty<User>(x => x.SchoolGrade);
        public ExcelPropety AvtivityStatus_Excel = ExcelPropety.CreateProperty<User>(x => x.AvtivityStatus);
        public ExcelPropety Md5Code_Excel = ExcelPropety.CreateProperty<User>(x => x.Md5Code);

	    protected override void InitVM()
        {
        }

    }

    public class UserImportVM : BaseImportVM<UserTemplateVM, User>
    {

    }

}
