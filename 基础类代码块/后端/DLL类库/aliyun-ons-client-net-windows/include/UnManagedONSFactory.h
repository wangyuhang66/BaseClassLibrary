#ifndef __UNMANAGEDONSFACTORY_H__
#define __UNMANAGEDONSFACTORY_H__

#include "UnManagedProducer.h"
//#include "PullConsumer.h"
#include "UnManagedPushConsumer.h"
#include "UnManagedONSChannel.h"

namespace ons {

class ONSCLIENT_API UnmanagedONSFactoryProperty{
public:
    UnmanagedONSFactoryProperty();
    virtual ~UnmanagedONSFactoryProperty();
    bool checkValidityOfFactoryProperties(std::string key, std::string value);
    void setSendMsgTimeout(int value);
    void setOnsChannel(UnmanagedONSChannel onsChannel);
    void setFactoryProperty(std::string key, std::string value);
    void setFactoryProperties(std::map<std::string, std::string> factoryProperties);
    std::map<std::string, std::string> getFactoryProperties() const;
    std::string getProducerId();
    std::string getConsumerId();
    std::string getPublishTopics();
    std::string getMessageModel();
    int     getMessageTimeout();
    int     getConsumeThreadNums();
    std::string getMessageContent();
    std::string getNameSrvAddr();
    std::string getNameSrvDomain();
    std::string getAccessKey();
    std::string getSecretKey();
    std::string getChannel();
    UnmanagedONSChannel  getOnsChannel();

    std::string getProducerIdName();
    std::string getConsumerIdName();
    std::string getPublishTopicsName();
    std::string getMsgContentName();
    std::string getAccessKeyName();
    std::string getSecretKeyName();
    std::string getOnsChannelName();
public:
    static const std::string ProducerId;
    static const std::string ConsumerId;
    static const std::string PublishTopics;
    static const std::string MsgContent;
    static const std::string ONSAddr;
    static const std::string AccessKey;
    static const std::string SecretKey;
    static const std::string MessageModel;
    static const std::string BROADCASTING;
    static const std::string CLUSTERING;
    static const std::string SendMsgTimeoutMillis;
    static const std::string NAMESRV_ADDR;
    static const std::string ConsumeThreadNums;
    static const std::string OnsChannel;//set ECS channel
private:    
    std::map<std::string, std::string> m_onsFactoryProperties;    
};    

class ONSCLIENT_API UnmanagedONSFactoryAPI{
public:
    UnmanagedONSFactoryAPI();
    virtual ~UnmanagedONSFactoryAPI();

    virtual UnmanagedProducer*     createProducer(UnmanagedONSFactoryProperty factoryProperty);
    //virtual PullConsumer* createPullConsumer(UnmanagedONSFactoryProperty factoryProperty);
    virtual UnmanagedPushConsumer* createPushConsumer(UnmanagedONSFactoryProperty factoryProperty);
};

class ONSCLIENT_API UnmanagedONSFactory{
public:
    virtual ~UnmanagedONSFactory();
    static UnmanagedONSFactoryAPI* getInstance(); 
private:
     UnmanagedONSFactory();
     static UnmanagedONSFactoryAPI *onsFactoryInstance;
};

}

#endif

