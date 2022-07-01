using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.MultiplePicturesVMs
{
    public partial class MultiplePicturesListVM : BasePagedListVM<MultiplePictures_View, MultiplePicturesSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("MultiplePictures", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<MultiplePictures_View>> InitGridHeader()
        {
            return new List<GridColumn<MultiplePictures_View>>{
                this.MakeGridHeader(x => x.Md5Code),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<MultiplePictures_View> GetSearchQuery()
        {
            var query = DC.Set<MultiplePictures>()
                .Select(x => new MultiplePictures_View
                {
				    ID = x.ID,
                    Md5Code = x.Md5Code,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class MultiplePictures_View : MultiplePictures{

    }
}
