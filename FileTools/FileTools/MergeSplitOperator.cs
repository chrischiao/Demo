using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileTools
{
    public static class MergeSplitOperator
    {
        private const string FirstFileKey = "F1";
        private const string SecondFileKey = "F2";

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <param name="destFile"></param>
        public static bool MergeFiles(string file1, string file2, string destFile)
        {
            try
            {
                if (!File.Exists(file1) || !File.Exists(file2))
                {
                    Console.WriteLine("file not exist");
                    return false;
                }

                IList<MemoryStream> memoryStreams = new List<MemoryStream>();
                memoryStreams.Add(GetFileInfoStream(file1, file2));
                memoryStreams.Add(new MemoryStream(File.ReadAllBytes(file1)));
                memoryStreams.Add(new MemoryStream(File.ReadAllBytes(file2)));

                FileStream fileStream = new FileStream(destFile, FileMode.Create);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, memoryStreams);
                fileStream.Close();

                // 删除原文件
                File.Delete(file1);
                File.Delete(file1);

                return true;
            }
            catch (Exception)
            {
                // TODO : 异常信息处理

                return false;
            }

        }

        /// <summary>
        /// 拆分文件
        /// </summary>
        /// <param name="filePath"></param>
        public static bool SplitFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false; // TODO : 信息提示

                int index = filePath.LastIndexOf(@"\", StringComparison.Ordinal);
                string directory = filePath.Substring(0, index + 1);

                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                IList<MemoryStream> memoryStreams = binaryFormatter.Deserialize(fileStream) as IList<MemoryStream>;
                if (memoryStreams != null && memoryStreams.Count == 3)
                {
                    Hashtable hashtable = GetFileInfoFromStream(memoryStreams[0]);
                    if (hashtable != null && hashtable.ContainsKey(FirstFileKey) && hashtable.ContainsKey(SecondFileKey))
                    {
                        MemoryStreamToFile(memoryStreams[1], Path.Combine(directory, hashtable[FirstFileKey].ToString()));
                        MemoryStreamToFile(memoryStreams[2], Path.Combine(directory, hashtable[SecondFileKey].ToString()));
                    }
                }
                fileStream.Close();

                // 删除原文件
                File.Delete(filePath);

                return true;
            }
            catch (Exception)
            {
                // TODO : 异常信息处理

                return false;
            }
        }

        /// <summary>
        /// 文件信息作为流输出
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        private static MemoryStream GetFileInfoStream(string file1, string file2)
        {
            int index = file1.LastIndexOf(@"\", StringComparison.Ordinal);
            string name1 = file1.Substring(index + 1, file1.Length - index - 1);
            index = file2.LastIndexOf(@"\", StringComparison.Ordinal);
            string name2 = file2.Substring(index + 1, file2.Length - index - 1);

            Hashtable hashtable = new Hashtable();
            hashtable.Add(FirstFileKey, name1);
            hashtable.Add(SecondFileKey, name2);

            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, hashtable); // 保存文件名及索引
            return memoryStream;
        }

        /// <summary>
        /// 解析文件信息流
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <returns></returns>
        private static Hashtable GetFileInfoFromStream(MemoryStream memoryStream)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            memoryStream.Position = 0;
            //memoryStream.Seek(0, SeekOrigin.Begin); // 此语句作用同上
            Hashtable fileInfo = binaryFormatter.Deserialize(memoryStream) as Hashtable;
            memoryStream.Close();
            return fileInfo;
        }

        /// <summary>
        /// 从流还原出文件
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="filePath"></param>
        private static void MemoryStreamToFile(MemoryStream memoryStream, string filePath)
        {
            byte[] buffer = new byte[memoryStream.Length];
            memoryStream.Read(buffer, 0, (int)memoryStream.Length);
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            fileStream.Write(buffer, 0, (int)memoryStream.Length);

            fileStream.Close();
            memoryStream.Close();

            // TODO : 使用using
        }
    }
}