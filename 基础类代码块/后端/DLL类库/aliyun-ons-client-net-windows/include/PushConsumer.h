#ifndef __PUSHCONSUMER_H__
#define __PUSHCONSUMER_H__

#include "UnManagedPushConsumer.h"
#include "UnManagedAction.h"
#include "MessageListener.h"

namespace ons {

public ref class PushConsumer{
        
    public:
        PushConsumer(UnmanagedPushConsumer *pushConsumer);
        virtual ~PushConsumer();

        void start();
        void shutdown();
        void subscribe(System::String^ topic, System::String^ subExpression,  MessageListener^% listener);
        UnmanagedAction consumeMsg(UnmanagedMessage value);
        delegate UnmanagedAction delegateForConsumeMsg(UnmanagedMessage value);
    private:
        PushConsumer();
        PushConsumer(PushConsumer% other);
        UnmanagedPushConsumer* m_pushConsumer;
        delegateForConsumeMsg^ m_delegate;
        MessageListener^ m_msgLisenter;
};

}
#endif