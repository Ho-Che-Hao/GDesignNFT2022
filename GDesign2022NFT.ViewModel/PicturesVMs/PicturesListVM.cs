using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using GDesign2022NFT.Model;


namespace GDesign2022NFT.ViewModel.PicturesVMs
{
    public partial class PicturesListVM : BasePagedListVM<Pictures_View, PicturesSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "", dialogWidth: 800),
                this.MakeStandardAction("Pictures", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], ""),
            };
        }


        protected override IEnumerable<IGridColumn<Pictures_View>> InitGridHeader()
        {
            return new List<GridColumn<Pictures_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Md5Code),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(Pictures_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }


        public override IOrderedQueryable<Pictures_View> GetSearchQuery()
        {
            var query = DC.Set<Pictures>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new Pictures_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Md5Code = x.Md5Code,
                    PhotoId = x.PhotoId,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Pictures_View : Pictures{

    }
}
