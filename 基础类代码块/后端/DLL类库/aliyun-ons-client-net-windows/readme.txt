*****************************************************************************************************************
This document description how to use ons client4net for windows.

1. After download aliyun-ons-client-net-windows.zip, untar it
	download address:http://onsteam.oss-cn-hangzhou.aliyuncs.com/aliyun-ons-client-net-windows.zip
2. aliyun-ons-client-net directory:
    lib:        ONSClient4CPP.lib/ONSClient4CPP.dll and ManagedONS.dll/ManagedONS.lib are compiled by VS2013
    include:    header files
    example:    producer/pushconsumer example
    readme:     description how to use ons client4cpp
3. please configure your project according to "ALIYUN_ONS_CLIENT_FOR_CPP_AND_NET_USER_GUIDE.docx"

=============Attentions==================================================================
Following is the mandatory interfaces for producer and consumer:

Mandatory interfaces of producer: 
Producer pProducer = onsfactory.getInstance().createProducer(...);
pProducer.start();//start must be called before excuting send and shutdown functions
pProducer.send(...);
pProducer.shutdown();


Mandatory interfaces of pushconsumer:
PushConsumer pConsumer = onsfactory.getInstance().createPushConsumer(...);
pConsumer.subscribe("test_topic", "tag", &msgListener);
pConsumer.start();//start must be called before excuting shutdown function
pConsumer.shutdown();


*******************************************************************************************************************
