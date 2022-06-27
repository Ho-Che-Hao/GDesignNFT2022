using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace GDesign2022NFT.Model
{
    public class User : BasePoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public new int ID { get; set; }

        [Display(Name = "名字")]
        [Required(ErrorMessage = "名字必填")]
        public string Name { set; get; }

        [Display(Name = "信箱")]
        [Required(ErrorMessage = "信箱必填")]
        public string Email { set; get; }

        [Display(Name = "連絡電話")]
        [Required(ErrorMessage = "電話必填")]
        public string Phone { set; get; }

        public ForeignerTypeEnum IsForeigner { set; get; }

        [Display(Name = "學校名稱")]
        [Required(ErrorMessage = "學校名稱必填")]
        public string SchoolName { set; get; }
        [Display(Name = "系所名稱")]
        [Required(ErrorMessage = "系所名稱必填")]
        public string SchoolDepartment { set; get; }
        [Display(Name = "就讀年級")]
        [Required(ErrorMessage = "就讀年級必填")]
        public string SchoolGrade { set; get; }

        public AvtivityStatus AvtivityStatus { set; get; }

        [Required(ErrorMessage = "代號不可為 Null")]
        public string Md5Code { set; get; }
    }

    public enum ForeignerTypeEnum{
        Native,
        Foreigner,        
    }

    public enum AvtivityStatus
    {
        NotAvtivity,
        Avtivity
    }
}
