using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;

namespace Lfz.AutoUpdater.Utitlies
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ZipHelper
    {
        /// <summary>
        /// ѹ��
        /// </summary>
        /// <param name="zipedFileName"></param>
        /// <param name="targetDirectory"></param>
        public static void UnZipFile(string zipedFileName, string targetDirectory)
        {
            UnZipFile(zipedFileName, targetDirectory, string.Empty, string.Empty);
        }

        /// <summary>
        /// ��ѹ���ļ�
        /// </summary>
        /// <param name="zipedFileName">Zip�������ļ�������D:\test.zip��</param>
        /// <param name="targetDirectory">��ѹ����Ŀ¼</param>
        /// <param name="password">��ѹ����</param>
        /// <param name="fileFilter">�ļ�����������ʽ</param>
        public static void UnZipFile(string zipedFileName, string targetDirectory, string password, string fileFilter)
        {
            using (Stream stream = File.OpenRead(zipedFileName))
            using (var archive = ArchiveFactory.Open(stream))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(targetDirectory,
                        ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                }
            }
        }

        public static bool CreateZipFile(string folder, string targetName)
        {
            var path = Path.GetDirectoryName(targetName);
            if (string.IsNullOrEmpty(path)) return false;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (var archive = ZipArchive.Create())
            {
                archive.AddAllFromDirectory(folder);
                archive.SaveTo(targetName, CompressionType.Deflate);
            }
            return true;
        }


        public static bool CreateZipFile(string sourseBaseDir, List<string> fileList, string targetName)
        {
            var path = Path.GetDirectoryName(targetName);
            if (string.IsNullOrEmpty(path)) return false;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (string.IsNullOrEmpty(sourseBaseDir)) return false;
            using (var archive = ZipArchive.Create())
            {
                var length = sourseBaseDir.Length;
                foreach (var file in fileList)
                {
                    if (file.Length <= length || !file.StartsWith(sourseBaseDir, StringComparison.OrdinalIgnoreCase) || !File.Exists(file)) continue;
                    var relativeFile = file.Substring(length).Trim('\\').Trim('/');
                    archive.AddEntry(relativeFile, file);
                }
                archive.SaveTo(targetName, CompressionType.Deflate);
            }
            return true;
        }
    }
}