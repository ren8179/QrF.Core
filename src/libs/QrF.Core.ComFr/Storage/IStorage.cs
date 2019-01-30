using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QrF.Core.ComFr.Storage
{
    public interface IStorage
    {
        string SaveFile(Stream stream, string fileName);
        Task<string> SaveFileAsync(Stream stream, string fileName);
        string SaveFile(Stream stream, string directory, string fileName);
        Task<string> SaveFileAsync(Stream stream, string directory, string fileName);
        void AppendFile(Stream stream, string filePath);
        Task AppendFileAsync(Stream stream, string filePath);
        void Delete(string filePath);
        void DeleteDirectory(string path);
    }
}
