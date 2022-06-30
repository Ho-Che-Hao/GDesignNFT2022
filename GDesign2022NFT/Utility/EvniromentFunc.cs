using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace GDesign2022NFT.Utility
{
    public class EvniromentFunc
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public EvniromentFunc(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetServerMappath(string input)
        {
           
            string webRootPath = _hostingEnvironment.ContentRootPath;
            var inputArr = input.Replace("/", "\\").Split("\\");
            var result = webRootPath;
            foreach (var item in inputArr)
            {

                if (!string.IsNullOrEmpty(item))
                {
                    //確保不會是空白
                    var itemPath = item.Trim();
                    switch (itemPath)
                    {
                        case ".":
                            //不動作 代表當下層
                            break;
                        case "..":
                            var webRootPathArr = result.Split("\\");
                            var webRootPathArrLength = webRootPathArr.Length;
                            if (webRootPathArrLength > 1)
                            {
                                result = string.Join("//", webRootPathArr.Take(webRootPathArrLength - 1));
                            }
                            //代表上一層，須將環境當下路徑去除尾段
                            break;
                        default:
                            result = $"{result}\\{itemPath}";
                            break;
                    }
                }
            }
            return result;
        }
    }
}
