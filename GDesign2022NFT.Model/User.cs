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
        [StringLength(50, ErrorMessage = "名字長度不可超過50字元")]
        [Required(ErrorMessage = "名字必填")]
        public string Name { set; get; }

        [Display(Name = "身分證字號/居留證號碼")]
        [Required(ErrorMessage = "身分證字號/居留證號碼必填")]
        public string IdentyCode { set; get; }

        [Display(Name = "信箱")]
        [Required(ErrorMessage = "信箱必填")]
        public string Email { set; get; }

        [Display(Name = "連絡電話")]
        [Required(ErrorMessage = "電話必填")]
        public string Phone { set; get; }

        public ForeignerTypeEnum IsForeigner { set; get; }

        [Display(Name = "學校名稱")]
        [StringLength(50,ErrorMessage = "學校名稱長度不可超過50字元")]
        [Required(ErrorMessage = "學校名稱必填")]
        public string SchoolName { set; get; }
        [Display(Name = "系所名稱")]
        [Required(ErrorMessage = "系所名稱必填")]
        [StringLength(50, ErrorMessage = "系所名稱長度不可超過50字元")]
        public string SchoolDepartment { set; get; }
        [Display(Name = "就讀年級")]
        [Required(ErrorMessage = "就讀年級必填")]
        [StringLength(10, ErrorMessage = "就讀年級長度不可超過10字元")]
        public string SchoolGrade { set; get; }

        public AvtivityStatus AvtivityStatus { set; get; }

        [Required(ErrorMessage = "代號不可為 Null")]
        public string Md5Code { set; get; }
    }

    public enum ForeignerTypeEnum{
        Native = 0,
        Foreigner = 1,        
    }

    public enum AvtivityStatus
    {
        NotAvtivity = 0,
        Avtivity = 1
    }
}
