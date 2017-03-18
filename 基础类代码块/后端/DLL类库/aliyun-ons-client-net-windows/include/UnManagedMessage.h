#ifndef __UNMANAGEDMESSAGE_H__
#define __UNMANAGEDMESSAGE_H__

#include <map>
#include <string>
#include <vector>
#include <sstream>
#include "ONSClient.h"

namespace ons {

    class ONSCLIENT_API SystemPropKey
    {
        public:
            SystemPropKey(){}
            ~SystemPropKey(){}
            static const std::string TAG;
            static const std::string KEY;
            static const std::string MSGID;
            static const std::string RECONSUMETIMES;
            static const std::string STARTDELIVERTIME;
    };

    //<!***************************************************************************
    class ONSCLIENT_API UnmanagedMessage
    {        
    public:
        UnmanagedMessage();
        UnmanagedMessage(const std::string& topic, const std::string& tags, const std::string& body);
        UnmanagedMessage(const std::string& topic, const std::string& tags, const std::string& keys, const std::string& body);

        virtual ~UnmanagedMessage();
        
        UnmanagedMessage(const UnmanagedMessage& other);
        UnmanagedMessage& operator=(const UnmanagedMessage& other);

        //userProperties was used to save user specific parameters which doesn't belong to SystemPropKey
        void   putUserProperties(const std::string& key, const std::string& value);
        std::string getUserProperties(const std::string& key) const;
        void   setUserProperties(std::map<std::string, std::string>& userProperty);
        std::map<std::string, std::string> getUserProperties() const;

       //systemProperties only save parameters defined in SystemPropKey, please do not add other parameters into systemProperties
        void   putSystemProperties(const std::string& key, const std::string& value);
        std::string getSystemProperties(const std::string& key) const;
        void   setSystemProperties(std::map<std::string, std::string>& systemProperty);
        std::map<std::string, std::string> getSystemProperties() const;
        
        std::string  getTopic()const;
        void    setTopic(const std::string& topic);
    
        std::string  getTag() const;
        void    setTag(const std::string& tags);
    
        std::string  getKey() const;
        void    setKey(const std::string& keys);

        std::string getMsgID() const;
        void   setMsgID(const std::string& msgId);
    
        __int64   getStartDeliverTime() const;
        void   setStartDeliverTime(__int64 level);
    
        std::string getBody()const;
        void   setBody(const std::string& msgbody);
    
        int    getReconsumeTimes() const;
        void  setReconsumeTimes(int reconsumeTimes);
        
        const std::string toString() const;

        const std::string toSystemString();

        const std::string toUserString();
    
    protected:
        void Init(const std::string& topic,
                  const std::string& tags,
                  const std::string& keys,
                  const std::string& body);
    
    
    private:
        std::string  topic;
        std::string  body;
        std::map<std::string, std::string> systemProperties;
        std::map<std::string, std::string> userProperties;
    };
    //<!***************************************************************************
    

}//<!end namespace;
#endif

