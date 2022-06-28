using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Attributes;

namespace GDesign2022NFT.Model
{
    //[Index(nameof(PicturesId))]
    //[MiddleTable]
    public class RelationUserPictures: TopBasePoco
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public new int ID { get; set; }

        public Pictures Pictures { set; get; }

        
        public int PicturesId { set; get; }

        public User Users { set; get; }

        public int UsersId { set; get; }
    }
}
