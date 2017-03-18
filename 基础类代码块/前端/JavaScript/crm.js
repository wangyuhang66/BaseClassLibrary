JS部分 拿到字段的值 var value =
    Xrm.Page.getAttribute("attributename").getValue();

Xrm.Page.getAttribute("attributename").setValue(value);
操作lookup字段值
    // Get a lookup value
    var lookup = new Array();

lookup = Xrm.Page.getAttribute("attributename").getValue();
if (lookup != null) {
  var name = lookup[0].name;
  var id = lookup[0].id;
  var entityType = lookup[0].entityType;
}

// Set a lookup value
var lookup = new Array();
lookup[0] = new Object();
lookup[0].id = recorid;
lookup[0].name = recordname;
lookup[0].entityType = entityname;
Xrm.Page.getAttribute("attributename").setValue(lookup);

Alternate method to set lookup value Xrm.Page.getAttribute("attributename")
    .setValue([ {id : recorid, name : recordname, entityType : entityname} ]);

拿到当前记录的id Xrm.Page.data.entity.getId();

拿到当前用户的id Xrm.Page.context.getUserId();

改变字段的require level
    Xrm.Page.data.entity.attributes.get('new_utilizationtype')
        .setRequiredLevel("required");
Xrm.Page.data.entity.attributes.get('new_adjustmenteffectiveweek')
    .setRequiredLevel("none");

货币字段赋值 Xrm.Page.data.entity.attributes.get("currencyfield ")
    .setValue(value);
Xrm.Page.data.entity.attributes.get("currencyfield ").getValue(value);

lookup字段赋值 var owner = new Array();
owner[0] = new Object();
owner[0].entityType = "systemuser";
owner[0].id = Xrm.Page.context.getUserId();
Xrm.Page.data.entity.attributes.get("new_fcaapprover").setValue(owner);

日期类型字段赋值 Xrm.Page.data.entity.attributes.get("new_fcaapproverejecttime")
    .setValue(new Date());

设置字段的保存模式 Xrm.Page.data.entity.attributes.get("new_fcaapprover")
    .setSubmitMode("always");

设置section的显示 / 隐藏 var secActual =
    Xrm.Page.ui.tabs.get("tabAdjustmentDetails").sections.get("secActual");
secActual.setVisible(false);

常用的保存数据的方法 Xrm.Page.data.refresh(true);

Xrm.Page.data.save().then(function() { successHandler(); },
                          function(error) {
                            Xrm.Utility.alertDialog(error.message);
                          });

在IFrame里边设置父页面(CRM页面)
    上字段的值 parent.Xrm.Page.data.entity.attributes.get("new_tagselected")
        .setValue("value");
Script操作数据 创建
    //创建
    SDK.REST.createRecord(account, "Account", function(account) {
      writeMessage("The account named \"" + account.Name +
                   "\" was created with the AccountId : \"" +
                   account.AccountId + "\".");
      writeMessage("Retrieving account with the AccountId: \"" +
                   account.AccountId + "\".");
      retrieveAccount(account.AccountId)
    }, errorHandler);
this.setAttribute("disabled", "disabled");
}

查询
    //查询
    function retrieveAccount(AccountId) {
      SDK.REST.retrieveRecord(AccountId, "Account", null,
                              null, function(account) {

                              }, errorHandler);
    }

或者是： function
ParticipantAliasChange() {
  var participantAlias =
      Xrm.Page.data.entity.attributes.get("new_participantalias").getValue();
  var option = "$filter=&$select=";

  var result = RetrieveMultipleRecords("", option);
} function RetrieveMultipleRecords(type, option) {
  var serverUrl = Xrm.Page.context.getServerUrl();
  var ODATA_ENDPOINT = "/XRMServices/2011/OrganizationData.svc";
  var ret;

  $.ajax({
    type : "GET",
    async : false,
    contentType : "application/json; charset=utf-8",
    datatype : "json",
    url : serverUrl + ODATA_ENDPOINT + "/" + type + "Set?" + option,
    beforeSend : function(XMLHttpRequest) {
      // Specifying this header ensures that the results will be returned as
      // JSON.
      XMLHttpRequest.setRequestHeader("Accept", "application/json");
    },
    success : function(data, textStatus, XmlHttpRequest) {
      ret = data.d.results;
    },

    error : function(XmlHttpRequest, textStatus, errorThrown) {}
  });

  return ret;
}

function RetrieveRecord(type, id) {
  var serverUrl = Xrm.Page.context.getServerUrl();
  var ODATA_ENDPOINT = "/XRMServices/2011/OrganizationData.svc";
  var ret;

  $.ajax({
    type : "GET",
    async : false,
    contentType : "application/json; charset=utf-8",
    datatype : "json",
    url : serverUrl + ODATA_ENDPOINT + "/" + type + "Set(guid'" + id + "')",
    beforeSend : function(XMLHttpRequest) {
      // Specifying this header ensures that the results will be returned as
      // JSON.
      XMLHttpRequest.setRequestHeader("Accept", "application/json");
    },
    success : function(data, textStatus, XmlHttpRequest) { ret = data.d; },

    error : function(XmlHttpRequest, textStatus, errorThrown) {

    }
  });

  return ret;
}

设置Tab中的节 Xrm.Page.ui.tabs.get("tab_1")
    .sections.get("tab_2_section_6")
    .setVisible(false); //隐藏
Xrm.Page.ui.tabs.get("tab_1")
    .sections.get("tab_2_section_6")
    .setVisible(true); //显示

设置字段只读 / 可编辑 Xrm.Page.getControl("new_systemuserid").setDisabled(true);
Xrm.Page.getControl("new_systemuserid").setDisabled(false);

设置字段可见 / 隐藏 Xrm.Page.getControl("new_systemuserid").setVisible(true);
Xrm.Page.getControl("new_systemuserid").setVisible(false);

设置字段赋值后自动提交 Xrm.Page.getAttribute("new_systemuserid")
    .setSubmitMode("always");

传递dialog的值给后边的页面 window.opener.window.parent.Xrm.Page.getAttribute(
                                                                   "ws_name")
    .setValue('fsdfs');




(!(~+[])+{})[--[~+""][+[]]*[~+[]] + ~~!+[]]+({}+[])[[~!+[]]*~+[]]