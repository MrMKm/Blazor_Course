function DotNetStaticMethodTest() {
    DotNet.invokeMethodAsync("TestProject.Client", "GetCurrentCount")
        .then(result => {
            console.log("Current Count from JS is " + result);
        });
}

function DotNetInstanceMethodTest(dotNetHelper) {
    dotNetHelper.invokeMethodAsync("IncrementCount");
}