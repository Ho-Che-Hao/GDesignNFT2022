using Microsoft.AspNetCore.Hosting;

namespace GIGABYTE.Utility.Utility
{
    public interface IFileRoot
    {
        string ServerPath(string input);
    }
    public class FileRoot : IFileRoot
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileRoot(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public string ServerPath(string input)
        {
            
            //string webRootPath = "E:\\WebsiteProgram\\2022NFT\\GDesign2022NFT\\GDesign2022NFT\\";
            string webRootPath = _hostingEnvironment.ContentRootPath;
            var temppath = Path.Combine(webRootPath, input);
            return temppath;
            /*var inputArr = input.Replace("/", "\\").Split("\\");
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
            return result;*/
        }
    }
}
