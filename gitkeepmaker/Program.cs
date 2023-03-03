using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitkeepmaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            //如果参数为空，则输入
            if (args.Length == 0)
            {
                //如果程序所在目录有.git文件夹，则设置路径为当前目录
                if (System.IO.Directory.Exists(".git"))
                {
                    path = System.IO.Directory.GetCurrentDirectory();
                }
                else
                {
                    Console.Write("请输入目录:");
                    path = Console.ReadLine();
                }
            }
            else
            {
            //获取第一个参数为目录
                path = args[0];
            }
            //获取目录下所有空文件夹
            List<string> emptyFolders = GetEmptyFolders(path);
            //遍历空文件夹，创建.gitkeep文件
            foreach (string folder in emptyFolders)
            {
                string gitkeep = folder + "\\.gitkeep";
                if (!System.IO.File.Exists(gitkeep))
                {
                    System.IO.File.Create(gitkeep);
                }
            }
        }
        /// <summary>
        /// 获取目录下所有空文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static List<string> GetEmptyFolders(string path)
        {
            List<string> emptyFolders = new List<string>();
            //获取目录下所有文件夹
            string[] folders = System.IO.Directory.GetDirectories(path);
            //如果当前文件夹没有文件，也没有子文件夹，则添加到空文件夹列表
            if (folders.Length == 0 && System.IO.Directory.GetFiles(path).Length == 0)
            {
                emptyFolders.Add(path);
            }
            //遍历文件夹
            foreach (string folder in folders)
            {
                //获取文件夹下所有文件
                string[] files = System.IO.Directory.GetFiles(folder);
                //如果文件夹下没有文件,也没有子文件夹
                if (files.Length == 0&& System.IO.Directory.GetDirectories(folder).Length == 0)
                {
                    //添加到空文件夹列表
                    emptyFolders.Add(folder);
                }
                //如果包含文件夹，则检查子文件夹
                else if (System.IO.Directory.GetDirectories(folder).Length > 0)
                {
                    //递归获取子文件夹下的空文件夹
                    emptyFolders.AddRange(GetEmptyFolders(folder));
                }
            }
            return emptyFolders;
        }
        
    }
}
