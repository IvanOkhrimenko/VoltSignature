using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.Files.Model
{
    public class FileModel
    {
        public FileModel(string name,byte[] data = null)
        {
            Name = name;
            Data = data;
        }

        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
