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
    public class MultiplePictures : PersistPoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public new int ID { get; set; }


        public string Md5Code { set; get; }
        [Display(Name = "圖片")]
        public List<MultiplePicturesUpload> Photos { get; set; }
    }

    public class MultiplePicturesUpload :TopBasePoco, ISubFile
    {
        public int MultiplePicturesId { get; set; }
        public MultiplePictures MultiplePictures { get; set; }
        //ISubFile定义的字段
        [Display(Name = "圖片")]
        public Guid FileId { get; set; }
        [Display(Name = "圖片")]
        public FileAttachment File { get; set; }
        public int Order { get; set; }
    }
}
