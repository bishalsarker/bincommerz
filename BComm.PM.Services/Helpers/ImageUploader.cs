using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Helpers
{
    public class ImageUploader
    {
        public string ImageUrl 
        { 
            get
            {
                return "/images/" + _productHashId + "_0" + _fileExtension;
            } 
        }

        private readonly string _productHashId;
        private readonly byte[] _fileByteArray = null;
        private readonly string _fileString;

        private string _fileExtension = null;
        private readonly List<string> _validExtentions = new List<string>() { ".jpg", ".jpeg", ".png" };

        private readonly IHostingEnvironment _env;

        public ImageUploader(string fileString, string productHashId, IHostingEnvironment env)
        {
            _env = env;
            _productHashId = productHashId;

            _fileString = fileString;
            _fileByteArray = Convert.FromBase64String(fileString);
            GetFileExtension();

            Validate();
        }

        public async Task UploadAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "images");
            var fileName = _productHashId + _fileExtension;

            await File.WriteAllBytesAsync(Path.Combine(path, fileName), _fileByteArray);
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
            if (_fileString.Contains("iVBORw0KGgo"))
            {
                _fileExtension = ".png";
            }
        }
    }
}
