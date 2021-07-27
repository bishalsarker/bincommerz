using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BComm.PM.Services.Helpers
{
    public class ImageInfo
    {
        public byte[] FileBytes
        {
            get
            {
                return _fileByteArray;
            }
        }

        public string Name
        {
            get
            {
                return _fileName;
            }
        }

        public string Extention
        {
            get
            {
                return _fileExtension;
            }
        }

        public string Directory
        {
            get
            {
                return _directory;
            }
        }

        private readonly string _fileName;
        private readonly byte[] _fileByteArray = null;
        private readonly string _fileString;

        private string _fileExtension = null;
        private readonly List<string> _validExtentions = new List<string>() { ".jpg", ".jpeg", ".png" };
        private readonly string _directory;

        public ImageInfo(string fileString, string fileName, IHostingEnvironment env)
        {
            _fileName = fileName;
            _directory = Path.Combine(env.WebRootPath, "images");
            _fileString = fileString;
            _fileByteArray = Convert.FromBase64String(fileString);
            GetFileExtension();

            Validate();
        }

        private void Validate()
        {
            if (_fileByteArray == null)
            {
                throw new InvalidOperationException("Invalid file: null");
            }

            if (_fileByteArray.Length < 0)
            {
                throw new InvalidOperationException("Invalid file: < 0");
            }

            if (!_validExtentions.Any(x => x == _fileExtension))
            {
                throw new InvalidOperationException("Invalid file: ext");
            }
        }

        private void GetFileExtension()
        {
            if (_fileString.ToUpper().Contains("IVBOR"))
            {
                _fileExtension = ".png";
            }

            if (_fileString.ToUpper().Contains("/9J/4"))
            {
                _fileExtension = ".jpg";
            }
        }
    }
}