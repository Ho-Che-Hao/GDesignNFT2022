using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.API.UserVMs
{
    public partial class UserApiListVM : BasePagedListVM<UserApi_View, UserApiSearcher>
    {

        protected override IEnumerable<IGridColumn<UserApi_View>> InitGridHeader()
        {
            return new List<GridColumn<UserApi_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.IdentyCode),
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

        public override IOrderedQueryable<UserApi_View> GetSearchQuery()
        {
            var query = DC.Set<User>()
                .Select(x => new UserApi_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    IdentyCode = x.IdentyCode,
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

    public class UserApi_View : User{

    }
}
