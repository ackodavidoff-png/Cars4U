using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Infrastructure.Utilities.Contracts;

namespace WebApplication1.Infrastructure.Utilities
{
    public class SlugGenerator : ISlugGenerator
    {
        public string GenerateEntitySlug(string name)
        {
            string[] nameParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(np => np.ToLowerInvariant()).ToArray();
            return string.Join(' ', nameParts);
        }
    }
}
