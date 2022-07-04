using GDesign2022NFT.Model;
namespace GDesign2022NFT.ViewModel.UserDataVMs
{
    public class UserPostDataVM
    {
        public string Name { set; get; }

        public string IdentyCode { set; get; }

        public string Email { set; get; }

        public string Phone { set; get; }

        public bool IsForeigner { set; get; }
        public string SchoolName { set; get; }

        public string SchoolDepartment { set; get; }

        public string SchoolGrade { set; get; }
        public UserPostDataVM()
        {
           
        }
    }
}
