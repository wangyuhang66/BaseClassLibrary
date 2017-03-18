using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ons;

namespace ons
{
    class onscsharp
    {

        static void Main(string[] args)
        {
            //producer创建和正常工作的参数，必须输入
            ONSFactoryProperty factoryInfo = new ONSFactoryProperty();
            factoryInfo.setFactoryProperty(factoryInfo.getProducerIdName(), "PID_xxxxxxx");//在ONS控制台申请的producerId
            factoryInfo.setFactoryProperty(factoryInfo.getPublishTopicsName(), "xxxxxxxxx");// 在ONS 控制台申请的msg topic
            factoryInfo.setFactoryProperty(factoryInfo.getMsgContentName(), "input msg content");//消息内容
            factoryInfo.setFactoryProperty(factoryInfo.getAccessKeyName(), "xxxxx");//ONS AccessKey
            factoryInfo.setFactoryProperty(factoryInfo.getSecretKeyName(), "xxxxxxxxxxxxxxxx");// ONS SecretKey
			factoryInfo.setOnsChannel(ONSChannel.CLOUD);//默认值为ONSChannel.ALIYUN，聚石塔用户必须设置为CLOUD，阿里云用户不需要设置(如果设置，必须设置为ALIYUN)
			
            //创建producer       
            ONSFactory onsfactory = new ONSFactory();
            Producer pProducer = onsfactory.getInstance().createProducer(factoryInfo);

            //在发送消息前，必须调用start方法来启动Producer，只需调用一次即可。   
            pProducer.start();

            Message msg = new Message(
                //Message Topic
                        factoryInfo.getPublishTopics(),
                //Message Tag,可理解为Gmail中的标签，对消息进行再归类，方便Consumer指定过滤条件在ONS服务器过滤        
                        "TagA",
                //Message Body，任何二进制形式的数据，ONS不做任何干预，需要Producer与Consumer协商好一致的序列化和反序列化方式
                        factoryInfo.getMessageContent()
            );

            // 设置代表消息的业务关键属性，请尽可能全局唯一。
            // 以方便您在无法正常收到消息情况下，可通过ONS Console查询消息并补发。
            // 注意：不设置也不会影响消息正常收发
            msg.setKey("ORDERID_100");

            //发送消息，只要不抛异常就是成功    
			try
			{
				SendResultONS sendResult = pProducer.send(msg);
			}
			catch(ONSClientException e)
			{
				//异常处理
			}
			
			// 在应用退出前，必须销毁Producer对象，否则会导致内存泄露等问题
			pProducer.shutdown();

        }
    }
}
