using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ons;
using std;

namespace ons
{
	//pushConsumer拉取到消息后，会主动调用该实例的consume 函数
	public class MyMsgListener : MessageListener
    {    
        public MyMsgListener()
        {
        }

        ~MyMsgListener()
        {
        }

        public override  Action  consume(ref Message value)
        {//形参value是返回的消息实例，可以根据业务逻辑提取Message的各个字段
            Console.WriteLine("\nCallback topic: {0}, tag:{1}, key:{2}, msgId:{3},msgbody:{4}",
                value.getTopic(),value.getTag(),value.getKey(),value.getMsgID(),value.getBody());
            return ons.Action.CommitMessage;
        }
    }


    class onscsharp
    {
		static void Main(string[] args)
        {
            //producer创建和正常工作的参数，必须输入
            ONSFactoryProperty factoryInfo = new ONSFactoryProperty();           
            factoryInfo.setFactoryProperty(factoryInfo.getConsumerIdName(), "CID_xxxxxxxxx"); //在ONS控制台申请的consumerId
            factoryInfo.setFactoryProperty(factoryInfo.getPublishTopicsName(), "xxxxxxxx");// 在ONS 控制台申请的msg topic
            factoryInfo.setFactoryProperty(factoryInfo.getAccessKeyName(), "xxxxxxx");//ONS AccessKey
            factoryInfo.setFactoryProperty(factoryInfo.getSecretKeyName(), "xxxxxxxxxxx");// ONS SecretKey
			factoryInfo.setOnsChannel(ONSChannel.CLOUD);//默认值为ONSChannel.ALIYUN，聚石塔用户必须设置为CLOUD，阿里云用户不需要设置(如果设置，必须设置为ALIYUN)
			
            //创建consumer
            ONSFactory onsfactory = new ONSFactory();
            PushConsumer pConsumer = onsfactory.getInstance().createPushConsumer(factoryInfo);
            
            //register msg listener and subscribe msg topic
            MessageListener msgListener = new MyMsgListener();
            pConsumer.subscribe(factoryInfo.getPublishTopics(), "*",  ref msgListener);

            //在拉取消息前，必须调用start方法来启动consumer，只需调用一次即可。   
            pConsumer.start();   
			//consumer启动后，会自动拉取消息，拉取到消息后，会自动调用MyMsgListener实例的consume函数；     
                 
			//确定消费完成后，调用shutdown函数；在应用退出前，必须销毁Consumer 对象，否则会导致内存泄露等问题
            pConsumer.shutdown();
        }

    }
}
