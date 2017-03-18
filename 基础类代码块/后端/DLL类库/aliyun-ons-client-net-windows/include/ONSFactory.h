#ifndef __ONSFACTORY_H__
#define __ONSFACTORY_H__

#include "UnManagedONSFactory.h"
#include "Producer.h"
#include "PushConsumer.h"
#include "ONSChannel.h"

namespace ons{

    public ref class ONSFactoryProperty{
    public:
        ONSFactoryProperty();
        virtual ~ONSFactoryProperty();
        !ONSFactoryProperty();
        void setSendMsgTimeout(int value);
        void setOnsChannel(ONSChannel onsChannel);
        void setFactoryProperty(System::String ^ key, System::String ^ value);
        void setFactoryProperties(std::map<std::string, std::string>& factoryProperties);
        std::map<std::string, std::string> getFactoryProperties();
        System::String^ getProducerId();
        System::String^ getConsumerId();
        System::String^ getPublishTopics();
        int                     getMessageTimeout();
        ONSChannel       getOnsChannel();
        System::String^ getMessageContent();
        System::String^ getAccessKey();
        System::String^ getSecretKey();

        System::String^ getProducerIdName();
        System::String^ getConsumerIdName();
        System::String^ getPublishTopicsName();
        System::String^ getMsgContentName();
        System::String^ getAccessKeyName();
        System::String^ getSecretKeyName();
        System::String^ getOnsChannelName();

    private:
        UnmanagedONSFactoryProperty *m_ONSFactoryProperty;
};    

public ref class ONSFactoryAPI{
    public:
        ONSFactoryAPI();
        ONSFactoryAPI(UnmanagedONSFactoryAPI *other);
        ONSFactoryAPI(ONSFactoryAPI% other);
        virtual ~ONSFactoryAPI();
        Producer^     createProducer(ONSFactoryProperty^ factoryProperty);
        PushConsumer^ createPushConsumer(ONSFactoryProperty^ factoryProperty);

    private:
        UnmanagedONSFactoryAPI *m_ONSFactoryAPI;
};

public ref class ONSFactory{
    public:
        ONSFactory();
        ONSFactory(ONSFactory% other);
        virtual ~ONSFactory();
        ONSFactoryAPI^ getInstance();
    private:
        ONSFactoryAPI^ onsFactoryInstance;
};

}

#endif
