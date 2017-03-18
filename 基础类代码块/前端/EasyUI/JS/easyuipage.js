一： 使用 datagrid 默认机制  分页

后台：

public JsonResult GetQuestionUnit() {
    // easyui datagrid 自身会通过 post 的形式传递 rows and page
    int pageSize = Convert.ToInt32(Request["rows"]);
    int pageNum = Convert.ToInt32(Request["page"]);

    var dal = new QsQuestionUnitDal();
    var questionUnits = dal.GetList("", pageNum - 1, pageSize);
    // 返回到前台的值必须按照如下的格式包括 total and rows
    var easyUIPages = new Dictionary < string,
        object > ();
    easyUIPages.Add("total", questionUnits.FirstOrDefault() == null ? 0 : questionUnits.FirstOrDefault().ReqCount);
    easyUIPages.Add("rows", questionUnits);

    return Json(easyUIPages, JsonRequestBehavior.AllowGet);
}

前台：

    (function() {
            (function() {
                ('#dgd').datagrid({
                    pageNumber: 1,
                    //url: "@ViewBag.Domain/Paper/GetQuestionUnit?arg1=xxx",
                    columns: [
                        [{
                            field: 'Id',
                            title: 'id',
                            width: 100
                        }, {
                            field: 'Name',
                            title: 'name',
                            width: 100
                        }, ]
                    ],
                    pagination: true,
                    rownumbers: true,

                    pageList: [3, 6]
                });

                var p = ('#dgd').datagrid('getPager');
                ('#dgd').datagrid('getPager');
                (p).pagination({
                    beforePageText: '第', //页数文本框前显示的汉字
                    afterPageText: '页    共 {pages} 页',
                    displayMsg: '共{total}条数据',

                });
            });

            你需要把('#dgd').datagrid 方法放置到

            $(function() {

            });

            如果企图通过其它的 JS 方法来调用('#dgd').datagrid 方法， 则不会得到正确的分页结果。

            可以看到， 上面 JS 代码中 url 这一行是被注释掉了。 如果我们不需要做别的操作， 页面一加载就打算查询出数据， 则可以不注释掉该代码。 但是， 往往， 有的时候， url 的参数， 如 arg1 的值需要在界面上进行某些操作， 然后再通过 JS 代码去得到的， 这个时候， 就应该注释掉 url， 而改由在别的地方赋值， 如：

            var step1Ok = function() {

                $('#dgd').datagrid({
                    url: "@ViewBag.Domain/Paper/GetQuestionUnit?arg1=xxx",
                });

            };

            在上面的代码中， 我们可以假设是点了界面的某个按钮， 调用了 step1Ok 这个方法后， 才会去 url 查询数据， 并呈现到 UI 中去。
