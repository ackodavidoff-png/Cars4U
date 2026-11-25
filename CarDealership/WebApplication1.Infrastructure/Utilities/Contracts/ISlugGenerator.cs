using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Infrastructure.Utilities.Contracts
{
    public interface ISlugGenerator
    {
        string GenerateEntitySlug(string name);
    }
}
