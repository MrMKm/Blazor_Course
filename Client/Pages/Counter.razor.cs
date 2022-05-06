using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.Client.Pages
{
    public partial class Counter
    {
        [Inject]
        ServiceSingleton singleton { get; set; }

        [Inject]
        ServiceTransient transient { get; set; }

        [Inject]
        IJSRuntime js { get; set; }

        private int currentCount = 0;
        private static int currentCountStatic = 0;

        private async Task IncrementCountFromJS()
        {
            await js.InvokeVoidAsync("DotNetInstanceMethodTest", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        private async Task IncrementCount()
        {
            currentCount++;
            currentCountStatic++;
            singleton.Value = currentCount;
            transient.Value = currentCount;

            await js.InvokeVoidAsync("DotNetStaticMethodTest");
        }

        [JSInvokable]
        public static Task<int> GetCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
