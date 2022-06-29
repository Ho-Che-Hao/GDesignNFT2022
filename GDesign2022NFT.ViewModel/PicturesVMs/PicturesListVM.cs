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
        public PicturesListVM(){
           //是否需要分頁
            //NeedPage = false,
        }
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                //todo: 自行新增按鈕
                //this.MakeAction("ControllerName","ActionName","名稱","彈跳名稱",GridActionParameterTypesEnum.NoId,"自定義",600).SetIconCls("layui-icon layui-icon-add-1"),
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
                this.MakeGridHeader(x => x.Md5Code),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(Pictures_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                //todo: 自定義圖片列表顯示內容
                ColumnFormatInfo.MakeHtml($"<img src='{entity.PhotoPath}'>"),
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                //ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }


        public override IOrderedQueryable<Pictures_View> GetSearchQuery()
        {
            var query = DC.Set<Pictures>()
                .Select(x => new Pictures_View
                {
				    ID = x.ID,
                    Md5Code = x.Md5Code,
                    PhotoId = x.PhotoId,
                    PhotoPath = x.Photo.Path
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Pictures_View : Pictures{
        public string PhotoPath { set; get; }
    }
}
