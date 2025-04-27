(async () => {
    const bridge = chrome.webview.hostObjects.bridge;
    await bridge.Log("Extract script loaded. Bridge communication established.");
    const sleep = ms => new Promise(r => setTimeout(r, ms))
    await bridge.Log("Waiting for list fully loaded...");
    // Wait until list loaded
    while (document.getElementsByClassName("article-teaser-large").length == 0) {
        await sleep(100);
    }
    await bridge.Log("Finding webpack function...");
    let jsonpList = [];
    for (let js in webpackJsonp) {
        for (let num in webpackJsonp[js][1]) {
            const jsonpString = webpackJsonp[js][1][num].toString();
            if (jsonpString.indexOf("exports=JSON.parse('") > 0 && jsonpString.length > 1000000) {
                jsonpList.push(webpackJsonp[js][1][num])
            }
        }
    }
    console.log(jsonpList);
    await bridge.Log("Converting to Object...");
    let jsonObjList = [];
    for (let jsonp of jsonpList) {
        let tempObj = {};
        jsonp(tempObj);
        jsonObjList.push(tempObj.exports);
    }
    await bridge.Log("Parsing...");
    let jsonDict = {};
    const getName = obj => {
        let title = obj[0].metainfo.browser_title;
        if (title.indexOf("OUCH") >= 0) {
            return "OUCH";
        }
        if (title.indexOf("@RISK") >= 0) {
            return "RISK";
        }
        if (title.indexOf("NewsBites") >= 0) {
            return "NB";
        }
    }
    const format = await bridge.Format();
    if (format == 0) {
        format = null;
    }
    for (let obj of jsonObjList) {
        jsonDict[getName(obj)] = JSON.stringify(obj, null, format);
    }
    await bridge.Log("Writing to disk...");
    await bridge.WriteToDisk("ouch.json", jsonDict.OUCH);
    await bridge.WriteToDisk("atrisk.json", jsonDict.RISK);
    await bridge.WriteToDisk("newsbites.json", jsonDict.NB);
    await bridge.Log("Exiting...");
    await bridge.Exit();
})();