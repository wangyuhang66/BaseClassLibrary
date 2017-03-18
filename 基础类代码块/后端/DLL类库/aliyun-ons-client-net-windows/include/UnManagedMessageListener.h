#ifndef __UNMANAGEDMESSAGELISTENER_H__
#define __UNMANAGEDMESSAGELISTENER_H__

#include "UnManagedAction.h"
#include "UnManagedMessage.h"
#include "UnManagedConsumeContext.h"

namespace ons{

class ONSCLIENT_API UnmanagedMessageListener {
public:
    UnmanagedMessageListener(){}
    virtual ~UnmanagedMessageListener(){}
    
    //interface of consuming message, should be realized by application
    virtual UnmanagedAction consume(UnmanagedMessage message, UnmanagedConsumeContext context)
    {
        return UnmanagedCommitMessage;
    }

};

}
#endif
