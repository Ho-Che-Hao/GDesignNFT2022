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
    public class Pictures: PersistPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public new int ID { get; set; }

        [Display(Name = "圖片名稱")]
        //[Required(ErrorMessage = "圖片名稱必填")]
        //[RegularExpression("^(\\w)*(\\.)(\\w)*$", ErrorMessage = "檔案名稱只允許英文數字")]
        public string Name { set; get; }

        //public PicturesStatusEnum Status { set; get; }

        //[Required(ErrorMessage = "代號不可為 Null")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string Md5Code { set; get; }
        public FileAttachment Photo { set; get; }

        public Guid PhotoId { set; get; }


        //不使用在資料庫中的欄位
        [NotMapped]
        public string PicturePath
        {
            get
            {
                return $"/{this.Name}";
            }
        }

        //不使用在資料庫中的欄位
        [NotMapped]
        public string PictureMd5
        {
            get
            {
                return this.Photo == null ? "": this.Photo.FileData.ToString();
            }
        }
    }

    public enum PicturesStatusEnum
    {
        Delete,
        Avaliable
    }
}
