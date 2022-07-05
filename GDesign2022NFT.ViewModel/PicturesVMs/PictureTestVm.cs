using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDesign2022NFT.ViewModel.PicturesVMs
{
    public class PictureTestVm
    {
        //todo: 測試一般 class vm 取得 IHostingEnvironment
        private readonly IHostingEnvironment hostingEnvironment;
        public PictureTestVm(IHostingEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }

        public void Test()
        {
            
        }

    }
}
