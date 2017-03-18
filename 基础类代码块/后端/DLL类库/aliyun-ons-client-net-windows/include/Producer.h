#ifndef __PRODUCER_H__
#define __PRODUCER_H__


#include "UnManagedProducer.h"
#include "SendResultONS.h"
#include "Message.h"

namespace ons {

    public ref class Producer{       
    public:
  
        Producer(UnmanagedProducer *producer);
        virtual ~Producer();

        void start();
        SendResultONS^ send(Message^ msg);
        void shutdown();

    private:
        Producer();
        Producer(Producer% other);
        UnmanagedProducer *m_producer;
};

}
#endif
