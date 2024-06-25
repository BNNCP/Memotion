using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Models;

namespace memotion_core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}