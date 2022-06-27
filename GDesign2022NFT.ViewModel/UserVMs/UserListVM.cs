using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.UserVMs
{
    public partial class UserListVM : BasePagedListVM<User_View, UserSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("User", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("User", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<User_View>> InitGridHeader()
        {
            return new List<GridColumn<User_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Email),
                this.MakeGridHeader(x => x.Phone),
                this.MakeGridHeader(x => x.IsForeigner),
                this.MakeGridHeader(x => x.SchoolName),
                this.MakeGridHeader(x => x.SchoolDepartment),
                this.MakeGridHeader(x => x.SchoolGrade),
                this.MakeGridHeader(x => x.AvtivityStatus),
                this.MakeGridHeader(x => x.Md5Code),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<User_View> GetSearchQuery()
        {
            var query = DC.Set<User>()
                .Select(x => new User_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    IsForeigner = x.IsForeigner,
                    SchoolName = x.SchoolName,
                    SchoolDepartment = x.SchoolDepartment,
                    SchoolGrade = x.SchoolGrade,
                    AvtivityStatus = x.AvtivityStatus,
                    Md5Code = x.Md5Code,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class User_View : User{

    }
}
