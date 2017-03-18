#ifndef __UNMANAGEDPUSHCONSUMER_H__
#define __UNMANAGEDPUSHCONSUMER_H__

#include "UnManagedMessageListener.h"
namespace ons {


class ONSCLIENT_API UnmanagedPushConsumer{
        
public:
    UnmanagedPushConsumer(){} 
    virtual ~UnmanagedPushConsumer(){}
  
    virtual void start(){};
    virtual void shutdown(){}
    virtual void subscribe(const std::string& topic, const std::string& subExpression, UnmanagedMessageListener* listener){}
    void setGroupName(const std::string& groupName)	{
        m_groupName = groupName;    
    }   
    std::string getGroupName()	{
        return m_groupName; 
    }

private:	
    std::string m_groupName;
};

}
#endif

