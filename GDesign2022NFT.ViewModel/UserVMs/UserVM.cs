using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Model;
using System.Net.Mail;
using GDesign2022NFT.ViewModel.PicturesVMs;

namespace GDesign2022NFT.ViewModel.UserVMs
{
    public partial class UserVM : BaseCRUDVM<User>
    {
        public PictureSendUserVM SendPicture { set; get; }
        public UserVM()
        {
            
        }


        public void SetUserVMByMd5(string md5)
        {
            var vmUser = DC.Set<User>().FirstOrDefault(x => x.Md5Code == md5);
            var result = Wtm.CreateVM<UserVM>((vmUser == null) ? 0: vmUser.ID);
            this.SetEntity(result.Entity);
            this.InitVM();
        }

        protected override void InitVM()
        {
            SendPicture = GetSendPictureToUser(Entity.ID);
        }

        public override void Validate()
        {
           var item =  DC.Set<User>().FirstOrDefault(x => (x.Email.ToLower() == Entity.Email.ToLower()) || (x.IdentyCode.ToLower() == Entity.IdentyCode.ToLower()));
           if (item.Email.ToLower().Equals(Entity.Email.ToLower()))
           {
                MSD.AddModelError("Entity.Email","Email 已經報名過");
           }
           if (item.IdentyCode.ToLower().Equals(Entity.IdentyCode.ToLower()))
           {
                MSD.AddModelError("Entity.IdentyCode", "該 身分證/居留證 已經報名過");
           }

            if (!IsValidEmail(Entity.Email))
            {
                MSD.AddModelError("Entity.Email", "Email 格式不正確");
            }
           base.Validate();
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }

        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private PictureSendUserVM GetSendPictureToUser(int userId)
        {
            var relationPicture = DC.Set<RelationUserPictures>().AsQueryable();
            var Pictures = DC.Set<Pictures>().Where(x => x.IsValid).AsQueryable();
            PictureSendUserVM result = (from rel in relationPicture
                        join img in Pictures
                        on rel.PicturesId equals img.ID
                        where rel.UsersId == userId
                        select new PictureSendUserVM
                        {
                            PhotoId = img.ID,
                            PhotoPath = img.Photo.Path,
                            PhotoExt = img.Photo.FileExt,
                        }).FirstOrDefault();
            return result;
        }
    }
}
