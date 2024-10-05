using Microsoft.JSInterop;

namespace HardwareMon.UI.Utils
{
    internal class JSHelper
    {
        private readonly IJSRuntime _js;

        public JSHelper(IJSRuntime runtime)
        {
            _js = runtime;
        }
    }
}
