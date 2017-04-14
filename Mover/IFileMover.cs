using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mover
{
    public interface IFileMover
    {
        void Copy(string sourcePath, string targetPath, string fileName);
        void Delete(string targetPath, string fileName);
    }
}
