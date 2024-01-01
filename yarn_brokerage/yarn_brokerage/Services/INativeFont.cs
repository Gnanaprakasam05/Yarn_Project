using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace yarn_brokerage.Services
{
    public interface INativeFont
    {
        float GetNativeSize(float size);
        Task GrandPermission(string Permission);
    }
}
