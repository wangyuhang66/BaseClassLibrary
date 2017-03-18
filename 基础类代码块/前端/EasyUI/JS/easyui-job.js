//Tab 标签二次点击 隐藏/切换
$("#id").on("click", function () {
     $( "#DIVID" ).toggle();
 });

----------------------------------------------------------------------
 //Checkbox 默认选中
//在LangBo项目中 :
//全部加载成功后
onLoadSuccess: function (data) {
   if (data) {
       $.each(data.rows, function (index, item) {
           if (HotThreadMng.HotThread.indexOf(item.Id) != -1) {
               $( '#hotthreadlist' ).datagrid('checkRow', index);
           }
       });
   }
}
//基本：
onLoadSuccess: function (data)
{
    if (data) {
        $.each(data.rows, function (index, item)
        {
            if (item.IsHot) {
                $( '#hottaglist' ).datagrid('checkRow', index);
            }
        });
    }
}
-----------------------------------------------------------------------------
//获取当前列的Id:
$("#id").datagrid( "getSelected" ).Id
-------------------------------------------------------------------------------------
//EASYUI-分页控件
//数据加载完后:在分页
//分页加得属性
    fitColumns: false,
    rownumbers: true,
    singleSelect: true,
    idField: "Id",
    pagination: true,
    pageList: [10, 30, 50, 100]

//。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。。
pagination: true
var p = $('#userloginlist').datagrid( 'getPager');
$(p).pagination({
    pageSize: 10, //每页显示的记录条数，默认为10
    pageList: [5, 10], //可以设置每页记录条数的列表
    pageNumber: 1, //当分页建立时,显示的页数
    total: loginResult.LoginStatistics,
    beforePageText: '第',//页数文本框前显示的汉字
    afterPageText: '页    共 {pages} 页' ,
    displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录' ,
   //点击上一页,下一页
    onSelectPage: function (pag, rows) {
        $.ajax({
            type: "post" ,
            url: "/SystemUser/SeeUser/UserLogin" ,
            data: "{'startTime':'" + $("#startTime" ).datebox("getValue") + "'" + "," + "'endTime':'" + $("#endTime"                    ).datebox("getValue") + "'" + "," + "'pag':'" + pag + "'" + "," + "'rows':'" +rows+"'}",
            contentType: "application/json; charset=utf-8" ,
            dataType: "json" ,
            success: function (loginResult) {
            }
        });
    }
});
---------------------------------------------------------------------
//JS 精确度问题:注：自行百度了解一下
 cet4amouth += parseFloat(result[ "NewPlan" ][i].LLBAmount);
 cet4amouth = parseFloat(cet4amouth.toFixed(1));
 ------------------------------------------------------------------------

//Select:改变下拉框时触发事件:
 $("#Select").on( "change" ,function () {
//把相对应的input的值改变
$("#mailnotify .frame-list input[name=eFrom]").val("edu@" +result).ToLower();
}
//获取select选中值
$("#pop_BoBoCard select[name='TopicID']").val()

//slect赋值
for(vari = 0; i < result.length; i++) {
    $("#pop_TopicTeacher .popinner select[name=TopicID]").append($("<option>").text(result[i].*****).val(result[i].Id));
}
---------------------------------------------------------------------------------------------------------------------------------------
//双击
InitDropDownData:function() {
    $.ajax({
        type:"POST",
        url:"/Community/Topic/listCategory",
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(result) {
           //$("#pop_TopicTeacher .popinner select[name=TopicID]").empty();
           for(vari = 0; i < result.length; i++) {
                $("#pop_TopicTeacher .popinner select[name=TopicID]").append($("<option>").text(result[i].CategoryName).val(result[i].Id));
            }
        },
        error:function(ex) {

        }
    });
}
--------------------------------------------------------------------------
//直接赋值:
Controller:
 publicasyncTask<ActionResult> TopicTeacherMng()
{
   if(CurrentUser.GetCurrentUserPurview(CurrentUser.UserPurview_Community))
    {
       List<FR_CategoryInfo> listCategory =awaitCommunityTopicFacade.ListCategory().ConfigureAwait(false);
       returnView(listCategory);
    }
   else
       returnRedirect("~/Home/Error");
}

html:
@model List<FR_CategoryInfo>


 <td class="title">小组类型</td>
 <td class="content">
     <select name="CategoryID"value="">
         @foreach(FR_CategoryInfo category in Model)
          {
             <option value="@category.Id">@category.CategoryName</option>
          }
     </select>
 </td>
 ---------------------------------------------------------------------
 //EASYUI-Tabs
<div id="" class="easyui-tabs" style=" width: 100% ;" fit="true">
   <div title="话题" style=" padding: 20px ;">
        <div class="frame-list">
            <table id="topic"></table>
        </div>
    </div>
    <div title="背诵" style ="padding: 20px;">
        <div class="frame-list">
            <table id="repeat"></table>
        </div>
    </div>
</div>
//获取TabTitle:
  $("#tabs .tabs-header .tabs-inner").on("click" , function () {
            var tabTitle = $(this).text().trim()
            alert(tabTitle);
        });
//获取TabTitle:
 var tabTitle = $( "#tabs" ).tabs("getSelected").panel( 'options').title;
//隐藏tab
 $('#statis').tabs( 'getTab', "礼品赠送统计" ).panel('options').tab.hide();
-------------------------------------------------------------------------------------------------
//easyui  多选框

//多选属性值
singleSelect: false ,
checkOnSelect: true ,
selectOnCheck: true ,

 frozenColumns: [[
            { field:'ck', checkbox:true}
            ]],


[{
      text:"多选",
      iconCls:"icon-add",
      handler: TopicApplyMgr.ChangeApply
}]

ChangeApply:function() {
   var rows = $("#applylist").datagrid("getChecked");
   var appplyArr = [];
   for(var i = 0; i < rows.length; i++) {
        appplyArr.push(rows[i]);
    }

   var dataCarrier = {
        appplylist: appplyArr
    };
   if(appplyArr.length == 0) {
        alert("没有选中啊!!!!");
       returnfalse;
    }
   else{
        TopicApplyMgr.EditTopicApply(dataCarrier.appplylist);
        TopicApplyMgr.InitPageCrl();
    }
}
---------------------------------------------------------------------------
//toolbar:
//上边得操作
[{
    text:"审批",
    iconCls:"icon-add",
    handler:function() {
        TopicTeacherMgr.ChangeTeacherID();
        TopicTeacherMgr.SaveTopicTeacher();
    }
},"-", {
    text:"设为班长",
    iconCls:"icon-add",
    handler:function() {
        TopicTeacherMgr.SaveTaskAuth();o
    }
}]
-------------------------------------------------------------------------------
判断手风琴展开,闭合
第一种方案(不支持二级页面操作)
$(document).ready(function() {
    $("#layoutwest .nav a").each(function() {
       if(window.location.href.indexOf($(this).attr("href")) > -1) {
            $(this).parents("div[data-tab]").panel("expand");
        }
    })
})
第二种方案:
<divtitle="用户管理"data-options="collapsed:true"data-tab="usermgr">
</div>
$(document).ready(function() {
    openAccordionTab("mgr");
})
functionopenAccordionTab(tab) {
     $("#layoutwest div[data-tab="+ tab +"]").panel("expand");
}

第三种方案:
@{
        int [] purview = CurrentUser.GetCurrentUserPurview().Select(t => int .Parse(t.ToString())).ToArray();
    }
     <divtitle="运营管理"data-options="collapsed:@(purview[1]==1?"false":"true")">
     </div>
------------------------------------------------------------------------------------------------------------------------------
回调函数：
EditUserCourse:function() {
       this.ajaxCallBack =function(){
            $("#pop_UpdateUserCourse .popinner select[name=PaidCourseID]").val($("#usercourselist").datagrid("getSelected").PaidCourseID);
            $("#pop_UpdateUserCourse").dialog({ title:"编辑信息"});
            $("#pop_UpdateUserCourse").dialog("open");
        }
        UserCourseMgr.InitDropDownData($("#usercourselist").datagrid("getSelected").StartDate);
    },

    InitDropDownData:function(StartDate) {
       varself =this;
        $.ajax({
            type:"POST",
            url:"/SystemUser/UserCourse/ListCourseNameByUserID",
            data:"{'userEMail':'"+ $("#main .frame-top .searchbox").val() +"' "+","+" 'startDate':'"+ StartDate +"'}",
            dataType:"json",
            contentType:"application/json;charset=utf-8",
            success:function(result) {
                $("#pop_UpdateUserCourse .popinner select[name=PaidCourseID]").empty();
               for(vari = 0; i < result.length; i++) {
                    $("#pop_UpdateUserCourse .popinner select[name=PaidCourseID]").append($("<option>").text(result[i].CourseName).val(result[i].Id));
                }
                self.ajaxCallBack.call();
            },
            error:function(ex) {
                $("#pop_UpdateUserCourse").dialog("close");
                $.messager.alert('提示',"操作失败","error");
            }
        });
    }
--------------------------------------------------------------------------
排序
            sortName:"CurrStatus",
            sortOrder:"desc",
            remoteSort:false,
-----------------------------------------------------------------------------------
easyui中onDblClickRow双击
 onDblClickRow: function (rowIndex, rowData) {
    $.messager.alert( "内容详情" , rowData['Content' ], "");
}
--------------------------------------------------------------------------------------
//搜索框添加回车：
 $("#main .frame-top input").on("keydown" , function (ev){
            if (ev.keyCode == 13) {
                TrialCourseMng.ReLoad();
            }
        });


data-toggle ="enter" data-target="#main [data-act='search']"
------------------------------------------------------------------------------------------------------
easyui右键：
  /*为选项卡绑定右键*/
    $(".tabs li").live('contextmenu',function(e){

        /* 选中当前触发事件的选项卡 */
        var subtitle =$(this).text();
        $('#tabs').tabs('select',subtitle);

        //显示快捷菜单
        $('#menu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        return false;
    });

datagrid右键菜单代码：
onRowContextMenu: function(e, rowIndex, rowData) { //右键时触发事件
                //三个参数：e里面的内容很多，真心不明白，rowIndex就是当前点击时所在行的索引，rowData当前行的数据
                e.preventDefault(); //阻止浏览器捕获右键事件
                $(this).datagrid("clearSelections"); //取消所有选中项
                $(this).datagrid("selectRow", rowIndex); //根据索引选中该行
                $('#menu').menu('show', {
//显示右键菜单
                    left: e.pageX,//在鼠标点击处显示菜单                    top: e.pageY
                });
            }


当属性设置为"disabled"时，提交表单时，select的值无法传递，提交前移除disabled属性$("#role").removeAttr("disabled");

jquery添加属性$("#role").attr("disabled","disabled");   //select设置为只读
