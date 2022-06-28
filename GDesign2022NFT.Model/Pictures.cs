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
    public class Pictures : PersistPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public new int ID { get; set; }


        //[Required(ErrorMessage = "代號不可為 Null")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[RegularExpression("^$", ErrorMessage = "圖片已經上傳過")]
        public string Md5Code { set; get; }
        public FileAttachment Photo { set; get; }

        public Guid PhotoId { set; get; }
    }

    public enum PicturesStatusEnum
    {
        Delete,
        Avaliable
    }
}
