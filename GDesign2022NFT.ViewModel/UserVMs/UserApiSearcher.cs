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
    public partial class UserApiSearcher : BaseSearcher
    {
        [Display(Name = "信箱")]
        public String Email { get; set; }
        public ForeignerTypeEnum? IsForeigner { get; set; }
        public AvtivityStatus? AvtivityStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
