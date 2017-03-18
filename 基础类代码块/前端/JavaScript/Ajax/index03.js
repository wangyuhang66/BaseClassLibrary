 //Ajax 中的高级请求和响应

 //在回调函数中处理服务器的响应

 //状态：
 //0：请求未初始化（还没有调用 open()）。 
 //1：请求已经建立，但是还没有发送（还没有调用 send()）。 
 //2：请求已发送，正在处理中（通常现在可以从响应中获取内容头）。 
 //3：请求在处理中；通常响应中已有部分数据可用了，但是服务器还没有完成响应的生成。 
 //4：响应已完成；您可以获取并使用服务器的响应了。


 function updatePage() {
     if (request.readyState == 4) {
         if (request.status == 200) {
             var response = request.responseText.split("|");
             document.getElementById("order").value = response[0];
             document.getElementById("address").innerHTML = response[1].replace(/\n/g, "<br />");
         } else
             alert("status is " + request.status);
     }
 }

 //获取 0 就绪状态

 function getSalesData() {
     // Create a request object
     createRequest();
     alert("Ready state is: " + request.readyState);

     // Setup (initialize) the request
     var url = "/boards/servlet/UpdateBoardSales";
     request.open("GET", url, true);
     request.onreadystatechange = updatePage;
     request.send(null);
 }

 //使用服务器上返回的响应

 function updatePage() {
     if (request.readyState == 4) {
         var newTotal = request.responseText;
         var totalSoldEl = document.getElementById("total-sold");
         var netProfitEl = document.getElementById("net-profit");
         replaceText(totalSoldEl, newTotal);

         /* 图 out the new net profit */
         var boardCostEl = document.getElementById("board-cost");
         var boardCost = getText(boardCostEl);
         var manCostEl = document.getElementById("man-cost");
         var manCost = getText(manCostEl);
         var profitPerBoard = boardCost - manCost;
         var netProfit = profitPerBoard * newTotal;

         /* Update the net profit on the sales form */
         netProfit = Math.round(netProfit * 100) / 100;
         replaceText(netProfitEl, netProfit);
     }
 }

 //测试 responseText 属性  响应文本

 function updatePage() {
     // Output the current ready state
     alert("updatePage() called with ready state of " + request.readyState +
         " and a response text of '" + request.responseText + "'");
 }

 //忽略状态代码的回调函数

 function updatePage() {
     if (request.readyState == 4) {
         var response = request.responseText.split("|");
         document.getElementById("order").value = response[0];
         document.getElementById("address").innerHTML =
             response[1].replace(/\n/g, "<br />");
     }
 }