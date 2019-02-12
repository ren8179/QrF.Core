using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.Constant
{
    public static class FileExtensions
    {
        public static List<string> Video
        {
            get { return new List<string> { ".mp4", ".avi", ".rmvb" }; }
        }
        public static List<string> Zip
        {
            get { return new List<string> { ".rar", ".zip", ".7z" }; }
        }
        public static List<string> Pdf
        {
            get { return new List<string> { ".pdf" }; }
        }
        public static List<string> Txt
        {
            get { return new List<string> { ".txt" }; }
        }
        public static List<string> Doc
        {
            get { return new List<string> { ".doc", ".docx" }; }
        }
        public static List<string> Excel
        {
            get { return new List<string> { ".xls", ".xlsx" }; }
        }
        public static List<string> Ppt
        {
            get { return new List<string> { ".ppt", ".pptx" }; }
        }
    }
}
