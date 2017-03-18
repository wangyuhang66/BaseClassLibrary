#ifndef __UNMANAGEDPRODUCER_H__
#define __UNMANAGEDPRODUCER_H__


#include "UnManagedSendResultONS.h"
#include "UnManagedMessage.h"

namespace ons {

class ONSCLIENT_API UnmanagedProducer{
        
public:
  
    UnmanagedProducer(){}
    virtual ~UnmanagedProducer(){}
    
    //before send msg, start must be called to allocate resources.
    virtual void start(){}
    //before exit ons, shutdown must be called to release all resources allocated by ons internally.
    virtual void shutdown(){}
    //retry max 3 times if send failed. if no exception throwed, it sends success; 
    virtual UnmanagedSendResultONS send(UnmanagedMessage& msg)
    {
        return UnmanagedSendResultONS();
    }
    //virtual void setSendMsgTimeout(int value) = 0;
    //virtual void setNamesrvAddr(const std::string& nameSrvAddr) = 0;
};

}
#endif

