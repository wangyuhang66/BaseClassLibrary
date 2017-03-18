var xmlHttp = false;

function createXMLHttp()
{
    try //IE
    {
        xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
    }
    catch(ex)
    {
        try
        {
            xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch(ex)
        {
            xmlHttp = false;
        }
    }
    if(!xmlHttp && typeof XMLHttpRequest != "undefined")    //firefox etc.
    {
        xmlHttp = new XMLHttpRequest();
    }
}

//瑙ｆ瀽xml
  function onblu()
       {
            createXMLHttp();
            var postCode=document.getElementById("Text1").value;
            if(postCode.length>6)
            {
                return;
            }
            var url="WebService.asmx/GetArea?postCode="+postCode;
            xmlHttp.open("get",url,true);
            xmlHttp.onreadystatechange=readystate2;
            xmlHttp.send(null);
        }
        function readystate2()
        { 
 
            if(xmlHttp.readyState == 4 && xmlHttp.status == 200)
            {  
                var data=xmlHttp.responseXML.documentElement.firstChild.nodeValue;
               var arr=new Array();
               arr=data.split('|');
               for(var i=0;i<arr.length;i++)
               {
               document.getElementById("Text2").value=arr[i];
               document.getElementById("Text3").value=arr[i-1];
               }
            }
        }