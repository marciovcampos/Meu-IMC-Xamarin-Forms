using System;
using System.Collections.Generic;
using System.Text;

namespace CalcFreelancer.Domain.Interfaces
{
    public interface IDatabaseFile
    {
        string GetFilePath(string file);
    }
}
