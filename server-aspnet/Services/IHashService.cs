using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IHashService
    {
        Task<bool> Compare(string password, string hashedPassword);
    }
}