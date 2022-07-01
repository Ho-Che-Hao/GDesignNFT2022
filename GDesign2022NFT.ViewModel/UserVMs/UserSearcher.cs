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
    public partial class UserSearcher : BaseSearcher
    {
        [Display(Name = "信箱驗證狀況")]
        public SearchAvtivityStatus AvtivityStatus { set; get; }

        [Display(Name = "學校名稱")]
        public string SchoolName { set; get; }

        [Display(Name = "當地人/外國人")]
        public SearchForeignerTypeEnum IsForeigner { set; get; }

        [Display(Name = "信箱")]
        public string Email { set; get; }

        [Display(Name = "名字")]
        public string Name { set; get; }

        public enum SearchAvtivityStatus
        {
            [Display(Name = "全部")]
            All = 99,
            [Display(Name = "已驗證")]
            AvtivityStatus = 1,
            [Display(Name = "未驗證")]
            NotAvtivityStatus = 0,
        }

        public enum SearchForeignerTypeEnum
        {
            [Display(Name = "全部")]
            All = 99,
            [Display(Name = " 當地人")]
            Native = 0,
            [Display(Name = "外國人")]
            Foreigner = 1,
        }
        protected override void InitVM()
        {
            AvtivityStatus = SearchAvtivityStatus.All;
            IsForeigner = SearchForeignerTypeEnum.All;
        }

    }
}
