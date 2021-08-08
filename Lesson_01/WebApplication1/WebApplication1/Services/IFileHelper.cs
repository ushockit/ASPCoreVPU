using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public interface IFileHelper
    {
        string ReadAll(string path);
        void SaveAll(string path, string content);
    }
}
