#ifndef __MESSAGELISTENER_H__
#define __MESSAGELISTENER_H__

#include "UnManagedMessageListener.h"
#include "Message.h"
#include "ConsumeContext.h"
#include "Action.h"
#include <iostream>
namespace ons{

     struct pulledMessage{
        char    *topic;
        char    *tag;
        char    *key;
        char    *msgId;
        char    *msgBody;
        long     startDeliverTime;
        int        reconsumeTimes;
        char    *userProperty;
    };
    
    public ref class MessageListener
    {
    
    public:
        MessageListener();
        MessageListener(MessageListener% other);
        virtual ~MessageListener();
        !MessageListener();

        UnmanagedMessageListener* getMsgListener();

        virtual Action consume(Message^% message)
        {
            return (Action)UnmanagedCommitMessage;
        }

    private:
        UnmanagedMessageListener *m_messageListener;
    };

}

#endif